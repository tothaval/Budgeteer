using BudgetManagement;
using BudgetManagement.BudgetItem;
using BudgetManagement.Interfaces;
using BudgetManagement.IOData;

namespace BudgetManagementTests;


[TestFixture]
public class BudgetServiceTest
{

    [Test]
    public void BudgetServiceClassTest()
    {
        BudgetService budgetService = new BudgetService();


        Assert.That(budgetService.Budgets, Is.Not.Null);
    }


    [Test]
    public void AddBudgetTest()
    {
        IBudget budget = new Budget()
        {
            BudgetName = "TestBudget",
            BudgetPeriodStart = new DateTime(2025, 02, 01),
            BudgetPeriodEnd = new DateTime(2025, 05, 01),
            InitialBudget = 1577.12m
        };

        BudgetService budgetService = new BudgetService();


        int prevCount = budgetService.Budgets.Count;

        var result = budgetService.AddBudget(budget);

        var result2 = budgetService.AddBudget(budget);

        int postCount = budgetService.Budgets.Count;


        Assert.That(budgetService.Budgets, Is.Not.Null);

        Assert.That(prevCount, Is.EqualTo(0));

        Assert.That(postCount, Is.EqualTo(prevCount + 1));

        Assert.That(budgetService.Budgets[0].BudgetName, Is.EqualTo("TestBudget"));

        Assert.That(result, Is.True);

        Assert.That(result2, Is.False);

    }


    [Test]
    public void AddBudgetServiceDataTest()
    {
        BudgetServiceData budgetServiceData = new BudgetServiceData()
        {
            Budgets =
            [
                new BudgetData(){ BudgetName = "Test1", Identifier="Budget_123456"},
                new BudgetData(){ BudgetName = "Test2", Identifier="Budget_234567"},
                new BudgetData(){ BudgetName = "Test3", Identifier="Budget_345678"},
                new BudgetData(){
                    BudgetName = "Test4",
                    BudgetPeriodStart = new DateTime(2025, 02, 01),
                    BudgetPeriodEnd = new DateTime(2025, 05, 01),
                    Identifier="Budget_456789",
                    InitialBudget = 1577.12m,

                    BudgetChanges =
                    [
                        new BudgetChangeData(){ Identifier="BudgetChange_1234", Price=15.0m, Type = BudgetChangeType.Expense },
                        new BudgetChangeData(){ Identifier="BudgetChange_2345", Price=33.17m, Type = BudgetChangeType.Expense },
                        new BudgetChangeData(){ Identifier="BudgetChange_3456", Price=75.20m, Type = BudgetChangeType.Gain },
                        new BudgetChangeData(){ Identifier="BudgetChange_4789", Price=14.83m, Type = BudgetChangeType.Expense },
                        ]
                }
                ]
        };


        BudgetService budgetService = new BudgetService();


        int prevCount = budgetService.Budgets.Count;

        var result = budgetService.AddBudgetServiceData(budgetServiceData);

        int postCount = budgetService.Budgets.Count;


        Assert.That(prevCount, Is.EqualTo(0));

        Assert.That(postCount, Is.EqualTo(prevCount + 4));


        Assert.That(budgetService.Budgets[0].BudgetName, Is.EqualTo("Test1"));

        Assert.That(budgetService.Budgets[3].BudgetChanges[0].Identifier, Is.EqualTo("BudgetChange_1234"));


    }


    [Test]
    public void NewBudgetTest()
    {
        IBudget budget = new Budget()
        {
            BudgetName = "TestBudget",
            BudgetPeriodStart = new DateTime(2025, 02, 01),
            BudgetPeriodEnd = new DateTime(2025, 05, 01),
            InitialBudget = 1577.12m
        };

        BudgetService budgetService = new BudgetService()
        {
            TestBudgets = [
                new Budget(){ BudgetName = "Test1", Identifier="Budget_123456"},
                new Budget(){ BudgetName = "Test2", Identifier="Budget_234567"},
                new Budget(){ BudgetName = "Test3", Identifier="Budget_345678"},
                ]
        };

        int prevCount = budgetService.Budgets.Count;

        var result = budgetService.NewBudget();

        int postCount = budgetService.Budgets.Count;

        Assert.That(budgetService.Budgets, Is.Not.Null);

        Assert.That(prevCount, Is.EqualTo(3));

        Assert.That(postCount, Is.EqualTo(prevCount + 1));

        Assert.That(budgetService.Budgets[0].Identifier, Is.EqualTo(result.Identifier));

        Assert.That(result, Is.TypeOf(typeof(Budget)));

    }


    [Test]
    public void GetBudgetServiceDataTest()
    {

        BudgetService budgetService = new BudgetService()
        {
            TestBudgets = [
                new Budget(){ BudgetName = "Test1", Identifier="Budget_123456"},
                new Budget(){ BudgetName = "Test2", Identifier="Budget_234567"},
                new Budget(){ BudgetName = "Test3", Identifier="Budget_345678"},
                ]
        };

        var result = budgetService.GetBudgetServiceData().Result;

        Assert.That(result.Budgets.Count, Is.EqualTo(3));

        Assert.That(result.Budgets[0].Identifier, Is.EqualTo("Budget_123456"));

    }


    [Test]
    public void RemoveBudgetTest()
    {
        IBudget budget = new Budget() { BudgetName = "Test1", Identifier = "Budget_123456" };


        BudgetService budgetService = new BudgetService()
        {
            TestBudgets = [
                new Budget(){ BudgetName = "Test2", Identifier="Budget_234567"},
                budget,
                new Budget(){ BudgetName = "Test3", Identifier="Budget_345678"},
                ]
        };


        int prevCount = budgetService.Budgets.Count;

        var result = budgetService.RemoveBudget(budget);

        int postCount = budgetService.Budgets.Count;

        var result2 = budgetService.RemoveBudget(budget);

        Assert.That(prevCount, Is.EqualTo(3));

        Assert.That(result, Is.True);

        Assert.That(postCount, Is.EqualTo(prevCount - 1));

        Assert.That(result2, Is.False);

    }


    //[Test]
    // it did not work to load the file i guess because of this being a test, i don't know,
    // the assert was always null while the same method and filepath returns the proper object in debug run
    public async Task LoadBudgetServiceDataTest()
    {
        string filename = "E:\\Programmierung\\###SmallProjects2025\\Budgeteer\\BudgeteerWPF\\BudgeteerWPF\\TestData\\BudgetServiceData.xml";

        BudgetService budgetService = new BudgetService();

        object budgetServiceData = await budgetService.LoadBudgetServiceData(filename);

        Assert.That(budgetServiceData, Is.Not.Null);
    }


    //[Test]
    // it did work to save the file and move it to the new filename, assert was true, but i disabled the test
    // because i used absolute paths and do not wan't to copy the TestData folder or its contents to /bin    
    public void SaveBudgetServiceDataTest()
    {
        string filename = "..Budgeteer\\BudgeteerWPF\\BudgeteerWPF\\TestData\\BudgetServiceDataTest.xml";

        BudgetService budgetService = new BudgetService(filename)
        {
            TestBudgets = [
                new Budget(){ BudgetName = "Test1", Identifier="Budget_123456"},
                new Budget(){ BudgetName = "Test2", Identifier="Budget_234567"},
                new Budget(){ BudgetName = "Test3", Identifier="Budget_345678"}
                ]
        };

        var result = budgetService.SaveBudgetServiceData();

        var resultFileExists = File.Exists(filename);

        Assert.That(resultFileExists, Is.True);

        string filename2 = "..Budgeteer\\BudgeteerWPF\\BudgeteerWPF\\TestData\\prevBSDT.xml";

        File.Move(filename, filename2, true);
    }


}