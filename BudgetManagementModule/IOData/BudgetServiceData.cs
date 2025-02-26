using BudgetManagement.Interfaces;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace BudgetManagement.IOData;


[Serializable]
[XmlRoot("BudgetService")]
public class BudgetServiceData
{

    [XmlArray("Budgets")]
    public ObservableCollection<BudgetData> Budgets { get; set; } = new ObservableCollection<BudgetData>();


    public BudgetServiceData()
    {

    }

    public BudgetServiceData(BudgetService budgetService)
    {
        foreach (IBudget item in budgetService.Budgets)
        {
            BudgetData budgetData = new BudgetData(item);

            Budgets.Add(budgetData);
        }
    }


}
