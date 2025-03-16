using BudgetManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManagement
{
    public static class BudgetExtensionMethods
    {

        public static bool Compare(this IBudget budget, IBudget comparee)
        {
            if (budget is null || comparee is null) return false;

            return budget.InitialBudget == comparee.InitialBudget
                && budget.Identifier.Equals(comparee.Identifier)
                && budget.BudgetPeriodStart == comparee.BudgetPeriodStart
                && budget.BudgetPeriodEnd == comparee.BudgetPeriodEnd
                && budget.BudgetName.Equals(comparee.BudgetName)
                && budget.BudgetChanges.Equals(comparee.BudgetChanges);
        }


        public static bool Includes(this IEnumerable<IBudget> values, IBudget value )
        {
            if (values.Count() == 0)
            {
                return false;
            }            

            foreach (IBudget item in values)
            {
                if (item.Compare(value))
                {
                    return true;
                }
            }

            return false;

        }

    }
}
