using BudgetManagement;
using BudgetManagement.BudgetItem;
using BudgetManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagementTests;


[TestFixture]
public class BudgetManagementUtilityServiceTest
{

    [Test]
    public void CalculateValuesFromListTest()
    {

        IEnumerable<decimal> decimals = [15.05m, 33.24m, 19.29m, -55.87m];

        var result = BudgetManagementUtilityService.CalculateValuesFromList(decimals);

        Assert.That(result, Is.EqualTo(11.71m));
    }


    [Test]
    public void GetIdentifierTest()
    {

        IdentifierTestClass identifierTestClass = new IdentifierTestClass();

        var result = BudgetManagementUtilityService.GetIdentifier<IdentifierTestClass>();

        Assert.That(result.StartsWith("IdentifierTestClass_"));
    }


    [Test]
    public void ItemIsNotInListTest()
    {
        IEnumerable<decimal> decimals = [15.05m, 33.24m, 19.29m, -55.87m];

        Budget budget = new Budget() { BudgetName = "Test1", Identifier = "Budget_123456" };
        Budget budget2 = new Budget() { BudgetName = "Test4", Identifier = "Budget_999999" };

        IEnumerable<IBudget> budgets = [
            budget,
                new Budget(){ BudgetName = "Test2", Identifier="Budget_234567"},
                new Budget(){ BudgetName = "Test3", Identifier="Budget_345678"}
                ];

        var result = BudgetManagementUtilityService.ItemIsNotInList(15.00m, decimals);

        var result2 = BudgetManagementUtilityService.ItemIsNotInList(budget, budgets);
        var result3 = BudgetManagementUtilityService.ItemIsNotInList(budget2, budgets);

        Assert.That(result, Is.True);

        Assert.That(result2, Is.False);
        Assert.That(result3, Is.True);
    }


}


public class IdentifierTestClass
    {
    }