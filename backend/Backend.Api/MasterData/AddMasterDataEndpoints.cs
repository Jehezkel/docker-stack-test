using Backend.Api.DAL.Entities;
using Backend.Api.Endpoint;

namespace Backend.Api.MasterData;

public class AddMasterDataEndpoints
{
    public class CategoryDTO : IMapToEntity<CategoryDTO, CategoryEntity>
    {
        public string Name { get; set; } = default!;
        public CategoryEntity MapToEntity(CategoryDTO dto) => new CategoryEntity { Name = dto.Name };
    }

    public class ParameterDTO : IMapToEntity<ParameterDTO, ParameterEntity>
    {
        public string Name { get; set; } = default!;
        public ParameterEntity MapToEntity(ParameterDTO dto) => new ParameterEntity { Name = dto.Name };
    }

    public class ManufacturerDTO : IMapToEntity<ManufacturerDTO, ManufacturerEntity>
    {
        public string Name { get; set; } = default!;
        public ManufacturerEntity MapToEntity(ManufacturerDTO dto) => new ManufacturerEntity { Name = dto.Name };
    }

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var masterGroup = app.MapGroup("master-data");
            masterGroup.MapGroup("categories").WithTags("category")
                .AddCrudEndpoints<CategoryDTO, CategoryEntity>();
            masterGroup.MapGroup("manufacturers").WithTags("manufacturer")
                .AddCrudEndpoints<ManufacturerDTO, ManufacturerEntity>();
            masterGroup.MapGroup("parameters").WithTags("parameter")
                .AddCrudEndpoints<ParameterDTO, ParameterEntity>();
        }
   }
}