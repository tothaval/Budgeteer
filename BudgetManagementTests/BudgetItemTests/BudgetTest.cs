using BudgetManagement.BudgetChangeItem;
using BudgetManagement.BudgetItem;
using BudgetManagement.Interfaces;
using System.Collections.ObjectModel;

namespace BudgetManagementTests.BudgetItemTests;


[TestFixture]
public class BudgetTest
{

    [Test]
    public void IBudgetTest()
    {
        IBudget budget = new Budget() { InitialBudget = 15.0m };

        Assert.That(budget.Identifier, Is.TypeOf(typeof(string)));
        Assert.That(budget.Identifier.StartsWith("Budget_"));

        Assert.That(budget.BudgetName, Is.TypeOf(typeof(string)));
        Assert.That(budget.BudgetName, Is.EqualTo("new budget"));

        Assert.That(budget.InitialBudget, Is.TypeOf(typeof(decimal)));
        Assert.That(budget.InitialBudget, Is.EqualTo(15.0m));

        Assert.That(budget.BudgetPeriodStart, Is.TypeOf(typeof(DateTime)));
        Assert.That(budget.BudgetPeriodStart, Is.Not.Null);

        Assert.That(budget.BudgetPeriodEnd, Is.TypeOf(typeof(DateTime)));
        Assert.That(budget.BudgetPeriodEnd, Is.Not.Null);

        Assert.That(budget.BudgetChanges, Is.Not.Null);
        Assert.That(budget.BudgetChanges.Count, Is.EqualTo(0));
    }


    [Test]
    public void AddBudgetChangeTest()
    {
        BudgetChange budgetChange = new BudgetChange() { Price = 5.01m, Quantity = 4, Type = BudgetManagement.Interfaces.BudgetChangeType.Gain };

        BudgetChange budgetChange2 = new BudgetChange() { Price = 5.01m, Quantity = 4, Type = BudgetManagement.Interfaces.BudgetChangeType.Gain };

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m
            budgetChange2
            ];

        // arrange
        Budget budget = new Budget()
        {
            BudgetChanges = bugdetChanges,
        };

        int prevCount = budget.BudgetChanges.Count;

        // act
        var returnValue = budget.AddBudgetChange(budgetChange);

        var returnValue2 = budget.AddBudgetChange(budgetChange2);

        int postCount = budget.BudgetChanges.Count;


        // assert
        Assert.That(postCount, Is.EqualTo(prevCount + 1));

        Assert.That(budget.BudgetChanges[0].Identifier, Is.EqualTo(budgetChange.Identifier));

        Assert.That(returnValue.GetType(), Is.EqualTo(typeof(bool)));

        Assert.That(returnValue, Is.True);

        Assert.That(returnValue2, Is.False);
    }


    [Test]
    public void GetCurrentBalanceTest()
    {
        // mock data
        decimal initial = 1500.05m;

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m
            new BudgetChange(){ Price = 5.00m, Quantity = 4, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 20.00m
            ];

        decimal expectedResult = initial - 250.11m;

        // arrange
        Budget budget = new Budget()
        {
            InitialBudget = initial,
            BudgetChanges = bugdetChanges,
        };

        // act
        decimal result = budget.GetCurrentBalance();

        // assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }


    [Test]
    public void GetExpensesTest()
    {
        // mock data
        decimal initial = 1500.05m;

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m
            new BudgetChange(){ Price = 5.00m, Quantity = 4, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 20.00m
            ];

        decimal expectedResult = 350.11m;

        // arrange
        Budget budget = new Budget()
        {
            InitialBudget = initial,
            BudgetChanges = bugdetChanges,
        };

        // act
        decimal result = budget.GetExpenses();

        // assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }


    [Test]
    public void GetGainsTest()
    {
        // mock data
        decimal initial = 1500.05m;

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m
            new BudgetChange(){ Price = 5.01m, Quantity = 4, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 20.04m
            ];

        decimal expectedResult = 100.04m;

        // arrange
        Budget budget = new Budget()
        {
            InitialBudget = initial,
            BudgetChanges = bugdetChanges,
        };

        // act
        decimal result = budget.GetGains();

        // assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void NewBudgetChangeTest()
    {
        BudgetChange budgetChange = new BudgetChange() { Price = 5.01m, Quantity = 4, Type = BudgetManagement.Interfaces.BudgetChangeType.Gain };

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            budgetChange,
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m            
            ];

        // arrange
        Budget budget = new Budget()
        {
            BudgetChanges = bugdetChanges,
        };

        int prevCount = budget.BudgetChanges.Count;

        // act
        var returnValue = budget.NewBudgetChange();

        int postCount = budget.BudgetChanges.Count;


        // assert
        Assert.That(postCount, Is.EqualTo(prevCount + 1));

        Assert.That(budget.BudgetChanges[0].Identifier, Is.Not.EqualTo(budgetChange.Identifier));

        Assert.That(returnValue.GetType(), Is.EqualTo(typeof(BudgetChange)));
    }


    [Test]
    public void RemoveBudgetChangeTest()
    {
        // put into the list on last position
        BudgetChange budgetChange = new BudgetChange() { Price = 5.01m, Quantity = 4, Type = BudgetManagement.Interfaces.BudgetChangeType.Gain };

        // never put into the list
        BudgetChange budgetChange2 = new BudgetChange() { Price = 5.01m, Quantity = 4, Type = BudgetManagement.Interfaces.BudgetChangeType.Gain };

        ObservableCollection<IBudgetChange> bugdetChanges =
            [
            new BudgetChange(){ Price = 150.05m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 150.05m
            new BudgetChange(){ Price = 100.03m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Expense}, // result 200.06m
            new BudgetChange(){ Price = 50.00m, Quantity = 1, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 50.00m
            new BudgetChange(){ Price = 15.00m, Quantity = 2, Type=BudgetManagement.Interfaces.BudgetChangeType.Gain}, // result 30.00m
            budgetChange
            ];

        // arrange
        Budget budget = new Budget()
        {
            BudgetChanges = bugdetChanges,
        };

        int prevCount = budget.BudgetChanges.Count;

        // act
        var returnValue = budget.RemoveBudgetChange(budgetChange);
        var returnValue2 = budget.RemoveBudgetChange(budgetChange2);


        int postCount = budget.BudgetChanges.Count;

        // assert
        Assert.That(postCount, Is.EqualTo(prevCount - 1));

        Assert.That(budget.BudgetChanges, Has.Exactly(0).Matches<IBudgetChange>(bc => bc.Identifier == budgetChange.Identifier));
        Assert.That(budget.BudgetChanges, Has.Exactly(0).Matches<IBudgetChange>(bc => bc.Identifier == budgetChange2.Identifier));

        Assert.That(returnValue, Is.True);

        Assert.That(returnValue2, Is.False);
    }
}
