/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  Budget
 * 
 *  serializable data model class
 */
using System.Collections.ObjectModel;

namespace BudgetManagement.Interfaces;

public interface IBudget
{
    public DateTime BudgetPeriodStart { get; set; }
    public DateTime BudgetPeriodEnd { get; set; }

    public string Identifier { get; set; }
    public string BudgetName { get; set; }
    public decimal InitialBudget { get; set; }


    public ObservableCollection<IBudgetChange> BudgetChanges { get; init; }


    public bool AddBudgetChange(IBudgetChange budgetChange);

    public IBudgetChange NewBudgetChange();

    public decimal GetCurrentBalance();

    public decimal GetExpenses();

    public decimal GetGains();

    public bool RemoveBudgetChange(IBudgetChange budgetChange);

}
