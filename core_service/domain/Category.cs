using core_service.domain.@base;
using core_service.domain.valueobjects;
using core_service.services.Result;
using Color = core_service.domain.valueobjects.Color;

namespace core_service.domain;

public class Category : Entity, IDbModel
{
    public Name Name { get; private set; }
    public Color Color { get; private set; }

    public List<Category>? SubCategories { get; private set; } = null!;
    // TODO: Как насчет добавить иконку? 

    public Category(Guid id, Name name, Color color)
    {
        this.Id = id;
        this.Name = name;
        this.Color = color;
    }
    private Category() {}
    
    // Под вопросом, так как ещё не знаю вводить ли ограничение на уникальность цвета для каждой категории
    // TODO: Подумать
    public Result ChangeColor(Color color, IEnumerable<Category> categories)
    {
        var enumerable = categories.ToList();
        
        if(enumerable.FirstOrDefault(c => c.Color.Equals(color)) != null)
            return Result.Error("This color is already use");
        
        this.Color = color;
        
        return Result.Success();
    }

    public Result ChangeSubCategories(IEnumerable<Category>? subCategories)
    {
        if(subCategories == null)
            return Result.Error("SubCategories is null");
        
        this.SubCategories = subCategories.ToList();
        return Result.Success();
    }

    public Result AddSubCategory(Category category)
    {
        if(this.SubCategories?.FirstOrDefault(c => c.Id == category.Id) != null)
            return Result.Error("This category is already use");
        
        this.SubCategories?.Add(category);
        return Result.Success();
    }

    public Result RemoveSubCategory(Category category)
    {
        if(this.SubCategories?.FirstOrDefault(c => c.Id == category.Id) == null)
            return Result.Error("This category is not use");
        
        this.SubCategories.Remove(category);
        return Result.Success();
    }
    
    
    public DateTime CreatedAt { get; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; }
}