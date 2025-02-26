using BudgetManagement.BudgetChangeItem;
using BudgetManagement.Interfaces;
using System.Xml.Serialization;

namespace BudgetManagement.IOData;


[Serializable]
[XmlRoot("BudgetChange")]
public class BudgetChangeData
{

    public DateTime BudgetChangeDate { get; set; }


    public string Identifier { get; set; }


    public string Item { get; set; }


    public int Quantity { get; set; }


    public decimal Price { get; set; }


    public BudgetChangeType Type { get; set; }


    public BudgetChangeData()
    {

    }


    public BudgetChangeData(IBudgetChange budgetChange)
    {
        BudgetChangeDate = budgetChange.BudgetChangeDate;
        Identifier = budgetChange.Identifier;
        Item = budgetChange.Item;
        Quantity = budgetChange.Quantity;
        Price = budgetChange.Price;
        Type = budgetChange.Type;
    }


    public IBudgetChange GetBudgetChange() => new BudgetChange()
    {
        BudgetChangeDate = this.BudgetChangeDate,
        Identifier = this.Identifier,
        Item = this.Item,
        Quantity = this.Quantity,
        Price = this.Price,
        Type = this.Type
    };
}
