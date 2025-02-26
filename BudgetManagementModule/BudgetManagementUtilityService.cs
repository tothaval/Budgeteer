namespace BudgetManagement;

public static class BudgetManagementUtilityService
{

    public static decimal CalculateValuesFromList(IEnumerable<decimal> decimals)
    {
        decimal sum = 0.0m;

        if (decimals is null || decimals.Count() == 0)
            return sum;

        foreach (decimal item in decimals)
        {
            sum += item;
        }

        return sum;
    }


    public static string GetIdentifier<T>(T type)
    {
        string identifier = $"{type.GetType().Name}_{DateTime.Now.Ticks}";

        return identifier;
    }


    public static bool ItemIsNotInList<T>(T item, IEnumerable<T> list)
    {
        foreach (T checkThisItem in list)
        {
            if (checkThisItem.Equals(item))
                return false;
        }

        return true;
    }


}