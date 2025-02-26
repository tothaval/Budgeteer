using BudgetManagement.BudgetItem;
using BudgetManagement.Interfaces;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BudgetManagement.IOData;


[Serializable]
[XmlRoot("Budget")]
public class BudgetData
{
    public string BudgetName { get; set; }


    public DateTime BudgetPeriodStart { get; set; }


    public DateTime BudgetPeriodEnd { get; set; }


    public string Identifier { get; set; }


    public decimal InitialBudget { get; set; }


    [XmlArray("BudgetChanges")]
    public ObservableCollection<BudgetChangeData> BudgetChanges { get; set; } = new ObservableCollection<BudgetChangeData>();


    public BudgetData()
    {

    }

    public BudgetData(IBudget budget)
    {
        BudgetName = budget.BudgetName;
        BudgetPeriodStart = budget.BudgetPeriodStart;
        BudgetPeriodEnd = budget.BudgetPeriodEnd;
        Identifier = budget.Identifier;
        InitialBudget = budget.InitialBudget;

        foreach (IBudgetChange item in budget.BudgetChanges)
        {
            BudgetChangeData budgetChangeData = new BudgetChangeData(item);

            BudgetChanges.Add(budgetChangeData);
        }
    }

    public async Task<IBudget> GetBudget() => new Budget()
    {
        BudgetName = this.BudgetName,
        BudgetPeriodStart = this.BudgetPeriodStart,
        BudgetPeriodEnd = this.BudgetPeriodEnd,
        Identifier = this.Identifier,
        InitialBudget = this.InitialBudget,
        BudgetChanges = await GetBudgetChanges()
    };

    private Task<ObservableCollection<IBudgetChange>> GetBudgetChanges()
    {
        ObservableCollection<IBudgetChange> budgetChanges = new ObservableCollection<IBudgetChange>();

        foreach (BudgetChangeData item in BudgetChanges)
        {
            budgetChanges.Add(item.GetBudgetChange());
        }

        return Task.FromResult(budgetChanges);
    }

}
