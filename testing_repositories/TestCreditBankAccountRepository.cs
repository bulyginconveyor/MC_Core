using core_service.infrastructure.repository.enums;
using testing_repositories.@base;
using static NUnit.Framework.Assert;

namespace testing_repositories;

public class TestCreditBankAccountRepository : BaseCreditBankAccountRep
{
    [Test]
    public async Task LoadDataCreditBankAccount()
    {
        // Arrange
        var credit = OneCreditBankAccount();
        await _rep.Add(credit);
        await _rep.Save();

        var resGet = await _rep.GetOne(credit.Id);
        if(resGet.IsError)
            Fail();
        
        // Act
        var resLoad = await _rep.LoadData(resGet.Value);
        if (resLoad.IsError)
            Fail();

        // Assert
        AreEqual(credit, resLoad.Value);
    }
}