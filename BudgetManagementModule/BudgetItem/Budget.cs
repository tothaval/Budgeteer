using BudgetManagement.BudgetChangeItem;
using BudgetManagement.Interfaces;
using System.Collections.ObjectModel;

namespace BudgetManagement.BudgetItem;

public class Budget : IBudget
{
    public DateTime BudgetPeriodStart { get; set; }
    public DateTime BudgetPeriodEnd { get; set; }
    public string Identifier { get; set; }
    public decimal InitialBudget { get; set; }
    public ObservableCollection<IBudgetChange> BudgetChanges { get; init; } = new ObservableCollection<IBudgetChange>();
    public string BudgetName { get; set; }

    public Budget()
    {
        Identifier = BudgetManagementUtilityService.GetIdentifier(this);

        BudgetPeriodStart = DateTime.Now;
        BudgetPeriodEnd = DateTime.Now;
        InitialBudget = 0.0m;
        BudgetName = "new budget";
    }


    public bool AddBudgetChange(IBudgetChange budgetChange)
    {
        IEnumerable<string> budgetChangesIdentifierList = BudgetChanges.Select((o) => o.Identifier);

        if (BudgetManagementUtilityService.ItemIsNotInList(budgetChange.Identifier, budgetChangesIdentifierList))
        {
            BudgetChanges.Insert(0, budgetChange);

            return true;
        }

        return false;
    }


    public decimal GetCurrentBalance()
    {
        decimal balance = InitialBudget;

        foreach (var change in BudgetChanges)
        {
            balance += change.GetTotalPrice();
        }

        return balance;
    }


    public decimal GetExpenses()
    {
        // problem: it ignores quantity
        var values = BudgetChanges.Where((d) => d.Type.Equals(BudgetChangeType.Expense)).Select((d) => d.TotalPrice);

        return BudgetManagementUtilityService.CalculateValuesFromList(values);

    }


    public decimal GetGains()
    {
        var values = BudgetChanges.Where((d) => !d.Type.Equals(BudgetChangeType.Expense)).Select((d) => d.TotalPrice);

        return BudgetManagementUtilityService.CalculateValuesFromList(values);
    }


    public IBudgetChange NewBudgetChange()
    {
        IBudgetChange budgetChange = new BudgetChange();

        BudgetChanges.Insert(0, budgetChange);

        return budgetChange;
    }


    public bool RemoveBudgetChange(IBudgetChange budgetChange)
    {
        IEnumerable<string> budgetChangesIdentifierList = BudgetChanges.Select((o) => o.Identifier);

        if (!BudgetManagementUtilityService.ItemIsNotInList(budgetChange.Identifier, budgetChangesIdentifierList))
        {
            BudgetChanges.Remove(budgetChange);

            return true;
        }

        return false;
    }

}
