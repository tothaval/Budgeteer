using CommunityToolkit.Mvvm.ComponentModel;

namespace BudgeteerWPF.ViewModels;


public partial class MainViewModel : ObservableObject
{

    private BudgetServiceViewModel _BudgetServiceViewModel;


    public BudgetServiceViewModel BudgetServiceViewModel => _BudgetServiceViewModel;


    public MainViewModel(BudgetServiceViewModel budgetServiceViewModel)
    {
        _BudgetServiceViewModel = budgetServiceViewModel;
    }


}