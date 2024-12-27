namespace Backend.Api.DAL.Entities;

public class AppUserEntity
{
   public Guid AppUserId { get; set; } = default!;
   public string? Subject { get; set; }
   public string? Email { get; set; } 
   public string? Name { get; set; }
   public bool IsActive { get; set; } = false;
}
