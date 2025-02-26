using BudgetManagement.BudgetItem;
using BudgetManagement.Interfaces;
using BudgetManagement.IOData;
using FileIO;
using System.Collections.ObjectModel;

namespace BudgetManagement;

public class BudgetService
{

#if DEBUG
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public ObservableCollection<IBudget> TestBudgets
    {
        get => _Budgets;
        set
        {
            if (_Budgets != value)
            {
                _Budgets = value;
            }
        }
    }
#endif


    private string targetFilePath = string.Empty;

    private ObservableCollection<IBudget> _Budgets = new ObservableCollection<IBudget>();

    public ObservableCollection<IBudget> Budgets => _Budgets;


    public BudgetService(string? dataStoragefilePath = null)
    {
        targetFilePath = dataStoragefilePath;

        if (dataStoragefilePath is null || dataStoragefilePath.Equals(string.Empty))
        {
            targetFilePath = Path.GetDirectoryName(Environment.ProcessPath) + "\\BudgetServiceData.xml";
        }

        // data loading probably needs to be implemented in a better way, like file checking and so on.
        BudgetServiceData budgetServiceData = LoadBudgetServiceData(targetFilePath).Result;

        AddBudgetServiceData(budgetServiceData);

    }


    public bool AddBudget(IBudget budget)
    {
        var ids = Budgets.Select((b) => b.Identifier).ToList<string>();

        if (BudgetManagementUtilityService.ItemIsNotInList(budget.Identifier, ids))
        {
            _Budgets.Add(budget);
            return true;
        }

        return false;
    }


    public async Task AddBudgetServiceData(BudgetServiceData budgetServiceData)
    {
        if (budgetServiceData is null)
            return;

        foreach (BudgetData item in budgetServiceData.Budgets)
        {
            IBudget budget = await item.GetBudget();

            if (!_Budgets.Contains(budget))
            {
                _Budgets.Add(budget);
            }
        }

        return;
    }


    public async Task<BudgetServiceData> GetBudgetServiceData()
    {
        BudgetServiceData budgetServiceData = new BudgetServiceData(this);

        await Task.CompletedTask;

        return budgetServiceData;
    }


    public async Task<BudgetServiceData> LoadBudgetServiceData(string filePath)
    {
        BudgetServiceData budgetServiceData = null;

        if (filePath is null || targetFilePath.Equals(string.Empty) || !File.Exists(filePath))
        {
#if DEBUG
            Logger.Error("File not found: {0}", filePath);
#endif
            return null;
        }

        object loadedData = await new Load().LoadXml(typeof(BudgetServiceData), filePath);

        budgetServiceData = (BudgetServiceData)loadedData;


        return budgetServiceData;
    }


    public IBudget NewBudget()
    {
        IBudget budget = new Budget();

        _Budgets.Insert(0, budget);

        return budget;
    }


    public bool RemoveBudget(IBudget budget)
    {
        if (_Budgets.Contains(budget))
        {
            _Budgets.Remove(budget);

            return true;
        }

        return false;
    }


    public async Task SaveBudgetServiceData()
    {
        BudgetServiceData budgetServiceData = await GetBudgetServiceData();

        await new Save().SaveAsXml<BudgetServiceData>(budgetServiceData, targetFilePath);
    }


}