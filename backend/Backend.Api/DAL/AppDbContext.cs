using System.Linq.Expressions;
using Backend.Api.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;

namespace Backend.Api.DAL;

public class AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService)
    : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<AppUserEntity> Users => Set<AppUserEntity>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUser().ToString();
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedOn = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = GetCurrentUser().ToString();
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<UserOwnedEntity>())
        {
            if (entry.State == EntityState.Added)
                entry.Entity.UserId = GetCurrentUser();
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        ApplyUserOwnedFilters(builder);
    }

    private void ApplyUserOwnedFilters(ModelBuilder builder)
    {
        Expression<Func<UserOwnedEntity, bool>> filterExpr = en => en.UserId == currentUserService.UserId;
        foreach (var eType in builder.Model.GetEntityTypes())
        {
            if (eType.ClrType.IsAssignableTo(typeof(UserOwnedEntity)))
            {
                // modify expression to handle correct child type
                var parameter = Expression.Parameter(eType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter,
                    filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // set filter
                eType.SetQueryFilter(lambdaExpression);
            }
        }
    }

    private Guid GetCurrentUser() => currentUserService.UserId ?? throw new UnauthorizedAccessException();
}

public interface ICurrentUserService
{
    Guid? UserId { get; set; }
}

public class CurrentUserService : ICurrentUserService
{
    public Guid? UserId { get; set; } = null;
}