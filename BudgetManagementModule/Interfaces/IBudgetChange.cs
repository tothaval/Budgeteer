/*  BudgetWatcher (by Stephan Kammel, Dresden, Germany, 2024)
 *  
 *  Budget
 * 
 *  serializable data model class
 */
namespace BudgetManagement.Interfaces;

public enum BudgetChangeType
{
    Expense,
    Gain
}

public interface IBudgetChange
{
    public string Identifier { get; set; }

    public BudgetChangeType Type { get; set; }

    public DateTime BudgetChangeDate { get; set; }

    public string Item { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    /// <summary>
    /// returns Quantity * Price;
    /// </summary>
    public decimal TotalPrice { get; }

    /// <summary>
    /// <para>
    /// depending on BudgetChangeType
    /// </para>
    /// <para>
    /// Expense returns Quantity * Price * -1 
    /// </para>
    /// 
    /// <para>
    /// Gain returns Quantity * Price
    /// </para>
    /// 
    /// </summary>
    /// <returns></returns>
    public decimal GetTotalPrice();


}
