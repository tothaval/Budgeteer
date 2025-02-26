using BudgetManagement.BudgetChangeItem;
using BudgetManagement.Interfaces;

namespace BudgetManagementTests.BudgetChangeItemTests;


[TestFixture]
public class BudgetChangeItemTest
{

    [Test]
    public void IBudgetChangeTest()
    {

        IBudgetChange budgetChange = new BudgetChange();
        IBudgetChange budgetChange2 = new BudgetChange()
        {
            Item = "Test Gain",
            Price = 15.07m,
            Quantity = 2,
            Type = BudgetChangeType.Gain
        };

        Assert.That(budgetChange.Identifier, Is.TypeOf(typeof(string)));
        Assert.That(budgetChange.Identifier.StartsWith("BudgetChange_"));

        Assert.That(budgetChange2.Identifier, Is.TypeOf(typeof(string)));
        Assert.That(budgetChange2.Identifier.StartsWith("BudgetChange_"));

        Assert.That(budgetChange.Item, Is.TypeOf(typeof(string)));
        Assert.That(budgetChange.Item, Is.EqualTo(string.Empty));

        Assert.That(budgetChange2.Item, Is.TypeOf(typeof(string)));
        Assert.That(budgetChange2.Item, Is.EqualTo("Test Gain"));

        Assert.That(budgetChange.Price, Is.TypeOf(typeof(decimal)));
        Assert.That(budgetChange.Price, Is.EqualTo(0.0m));
        Assert.That(budgetChange.TotalPrice, Is.EqualTo(0.0m));

        Assert.That(budgetChange2.Price, Is.TypeOf(typeof(decimal)));
        Assert.That(budgetChange2.Price, Is.EqualTo(15.07m));
        Assert.That(budgetChange2.TotalPrice, Is.EqualTo(30.14m));

        Assert.That(budgetChange.Quantity, Is.TypeOf(typeof(int)));
        Assert.That(budgetChange.Quantity, Is.EqualTo(1));

        Assert.That(budgetChange2.Quantity, Is.TypeOf(typeof(int)));
        Assert.That(budgetChange2.Quantity, Is.EqualTo(2));

        Assert.That(budgetChange.BudgetChangeDate, Is.TypeOf(typeof(DateTime)));
        Assert.That(budgetChange.BudgetChangeDate, Is.Not.Null);

        Assert.That(budgetChange2.BudgetChangeDate, Is.TypeOf(typeof(DateTime)));
        Assert.That(budgetChange2.BudgetChangeDate, Is.Not.Null);

    }


    [Test]
    public void GetTotalPriceTest()
    {
        BudgetChange budgetChangeExpense = new BudgetChange()
        {
            Item = "Test Expense",
            Price = 15.07m,
            Quantity = 2,
            Type = BudgetChangeType.Expense
        };

        BudgetChange budgetChangeGain = new BudgetChange()
        {
            Item = "Test Gain",
            Price = 15.07m,
            Quantity = 3,
            Type = BudgetChangeType.Gain
        };


        decimal expectedPriceExpense = -30.14m;
        var resultExpense = budgetChangeExpense.GetTotalPrice();

        decimal expectedPriceGain = 45.21m;
        var resultGain = budgetChangeGain.GetTotalPrice();


        Assert.That(resultExpense, Is.EqualTo(expectedPriceExpense));
        Assert.That(resultGain, Is.EqualTo(expectedPriceGain));

    }

}