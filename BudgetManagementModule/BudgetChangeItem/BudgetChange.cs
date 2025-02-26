using BudgetManagement.Interfaces;

namespace BudgetManagement.BudgetChangeItem;

public class BudgetChange : IBudgetChange
{

    // until a proper object creation via factory or so is done, i'll leave this as required
    public string Identifier { get; set; }

    public BudgetChangeType Type { get; set; } = BudgetChangeType.Expense;

    public DateTime BudgetChangeDate { get; set; } = DateTime.Now;

    public string Item { get; set; } = string.Empty;

    public int Quantity { get; set; } = 1;

    public decimal Price { get; set; } = 0.0m;


    /// <summary>
    /// returns Quantity * Price;
    /// </summary>
    public decimal TotalPrice => Quantity * Price;


    public BudgetChange()
    {
        Identifier = BudgetManagementUtilityService.GetIdentifier(this);

    }


    /// <summary>
    /// depending on BudgetChangeType, this returns
    /// Quantity * Price * -1(Expense) || Quantity * Price (Gain)
    /// </summary>
    /// <returns></returns>
    public decimal GetTotalPrice()
    {
        if (Type.Equals(BudgetChangeType.Expense))
        {
            return TotalPrice * -1;
        }

        return TotalPrice;
    }


}
