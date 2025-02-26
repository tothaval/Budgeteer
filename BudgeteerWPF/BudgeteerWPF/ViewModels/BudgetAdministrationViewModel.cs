using CommunityToolkit.Mvvm.ComponentModel;

namespace BudgeteerWPF.ViewModels;

public class BudgetAdministrationViewModel : ObservableObject
{

    private BudgetViewModel _BudgetViewModel;
    public BudgetViewModel BudgetViewModel => _BudgetViewModel;


    public BudgetAdministrationViewModel(BudgetViewModel budgetViewModel)
    {
        _BudgetViewModel = budgetViewModel;


    }


}
