using core_service.domain;
using core_service.domain.models;
using core_service.domain.models.valueobjects;
using core_service.infrastructure.repository.interfaces;
using core_service.infrastructure.repository.postgresql.repositories;
using core_service.services.GuidGenerator;

namespace testing_repositories.@base;

public class BaseCategoryRep : BaseTest
{
    protected IDbRepository<Category> _rep;
    
    [SetUp]
    public virtual void Setup()
    {
        _rep = new CategoryRepository(_context);
    }

    protected IEnumerable<Category> AllCategories()
    {
        Name houseName = Name.Create("Дом и хозяйство");
        Color houseColor = Color.Parse("#FFF");
        Category house = new Category(GuidGenerator.GenerateByBytes(), houseName, houseColor);
        
        Name houseChemistryName = Name.Create("Бытовая химия");
        Color houseChemistryColor = Color.Parse("#FFA");
        Category houseChemistry = new Category(GuidGenerator.GenerateByBytes(), houseChemistryName, houseChemistryColor);
        
        Name everydayGoodsName = Name.Create("Повседневные товары");
        Color everydayGoodsColor = Color.Parse("#0FA");
        Category everydayGoods = new Category(GuidGenerator.GenerateByBytes(), everydayGoodsName, everydayGoodsColor);
        
        house.AddSubCategory(houseChemistry);
        house.AddSubCategory(everydayGoods);
        
        Name transportName = Name.Create("Транспорт");
        Color transportColor = Color.Parse("#00F");
        Category transport = new Category(GuidGenerator.GenerateByBytes(), transportName, transportColor);
        
        Name transportGazName = Name.Create("Топливо");
        Color transportGazColor = Color.Parse("#0AF");
        Category transportGaz = new Category(GuidGenerator.GenerateByBytes(), transportName, transportColor);
        
        Name transportSecurityName = Name.Create("Страховка");
        Color transportSecurityColor = Color.Parse("#9FF");
        Category transportSecurity = new Category(GuidGenerator.GenerateByBytes(), transportName, transportColor);
        
        transport.AddSubCategory(transportGaz);
        transport.AddSubCategory(transportSecurity);

        return [house, transport];
    }

    public Category OneCategory()
    {
        return AllCategories().First();
    }
    
}