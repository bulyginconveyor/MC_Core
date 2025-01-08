using core_service.domain.models;
using core_service.domain.models.@base;
using core_service.services.Result;

namespace core_service.application.rest_controllers.DTO;

public class DTOCategory()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public List<DTOCategory>? SubCategories { get; set; }

    public static implicit operator Category(DTOCategory dto)
    {
        if (dto is null)
            return null;

        var category =  new Category(dto.Id, domain.models.valueobjects.Name.Create(dto.Name),
            domain.models.valueobjects.Color.Parse(dto.Color));

        if (dto.SubCategories is null || dto.SubCategories.Count == 0)
            return category;

        var subCategories = dto.SubCategories.Select(c => (Category)c).ToList();
        category.ChangeSubCategories(subCategories);

        return category;
    }
    
    public static implicit operator DTOCategory(Category category)
    {
        if (category is null)
            return null;

        if (category.SubCategories is null || category.SubCategories.Count == 0)
            return CreateLight(category);
        
        return new DTOCategory
        {
            Id = category.Id,
            Name = category.Name.Value,
            Color = category.Color.Value,
            SubCategories = category.SubCategories.Select(c => (DTOCategory)c).ToList()
        };
    }

    public static DTOCategory CreateLight(Category category)
    {
        if(category is null)
            return null;

        return new DTOCategory
        {
            Id = category.Id,
            Name = category.Name.Value,
            Color = category.Color.Value,
            SubCategories = null
        };
    }
}
