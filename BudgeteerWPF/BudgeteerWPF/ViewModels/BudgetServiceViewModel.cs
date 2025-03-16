using BudgetManagement;
using BudgetManagement.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace BudgeteerWPF.ViewModels;

public partial class BudgetServiceViewModel : ObservableObject
{

#if DEBUG
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
#endif

    private BudgetService _BugdetService;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(RemoveBudgetCanExecute))]
    [NotifyCanExecuteChangedFor(nameof(RemoveBudgetCommand))]
    private BudgetViewModel _SelectedBudget;

    public bool RemoveBudgetCanExecute => SelectedBudget != null;
    
    [ObservableProperty]
    private ObservableCollection<BudgetViewModel> _Budgets = new ObservableCollection<BudgetViewModel>();

    public BudgetServiceViewModel(BudgetService bugdetService)
    {

#if DEBUG
        Logger.Debug("BudgetServiceViewModel(BudgetService bugdetService) constructor init.");
#endif

        _BugdetService = bugdetService;

        BuildBudgetViewModelCollection();

        AddBudgetIfEmpty();

        SelectedBudget = Budgets.First();

#if DEBUG
        Logger.Debug("BudgetServiceViewModel(BudgetService bugdetService) constructor end.");
#endif
    }

    private void BuildBudgetViewModelCollection()
    {

#if DEBUG
        Logger.Debug("BuildBudgetViewModelCollection start.");
#endif

        foreach (IBudget item in _BugdetService.Budgets)
        {
            Budgets.Add(new BudgetViewModel(item, this));
        }

#if DEBUG
        Logger.Debug("BuildBudgetViewModelCollection end.");
#endif

    }


    [RelayCommand]
    public Task AddBudget()
    {

#if DEBUG
        Logger.Debug("AddBudget start.");
#endif

        Budgets?.Insert(0, new BudgetViewModel(_BugdetService.NewBudget(), this)); // das bugdetitem aus dem budgetservice holen oder anders?

        SelectedBudget = Budgets?.First()!;

#if DEBUG

        Logger.Debug("Budgets:{0}", string.Join("", Budgets?.Select(x => $"\n{x.Identifier} - {x.BudgetName} : {x.InitialBudget} >> {x.CurrentBudget} :budget changes: {x.BudgetChanges.Count} :: {x.BudgetPeriodStart} - {x.BudgetPeriodEnd}")!));

        //foreach (BudgetViewModel item in Budgets)
        //{
        //    Logger.Debug("{0} - {1} : {2} >> {3} :budget changes: {4} :: {5} - {6}",
        //        item.Identifier,                // 0
        //        item.BudgetName,                // 1
        //        item.InitialBudget,             // 2
        //        item.CurrentBudget,             // 3
        //        item.BudgetChanges.Count,       // 4
        //        item.BudgetPeriodStart,         // 5
        //        item.BudgetPeriodEnd            // 6
        //        );
        //}

        Logger.Debug("AddBudget end.");
#endif

        return Task.CompletedTask;
    }

    public bool AddBudgetIfEmpty()
    {
#if DEBUG
        Logger.Debug("AddBudgetIfEmpty start.");
#endif
                
        if (Budgets.Count > 0) return false;

        AddBudget();

#if DEBUG
        Logger.Debug("AddBudgetIfEmpty end.");
#endif
        return true;
    }

    [RelayCommand(CanExecute = nameof(RemoveBudgetCanExecute))]
    public Task RemoveBudget()
    {

#if DEBUG
        Logger.Debug("RemoveBudget start.");
#endif

        /// make clean mvvm, move the messagebox.show to the view,
        /// use delegate command to invoke logic upon click
        /// implement on budgetChange as well

        MessageBoxResult result = MessageBox.Show(
                $"Delete selected budget?",
                "Remove Budget", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {

#if DEBUG
            Logger.Debug("Removing {0}..", SelectedBudget.Identifier);
#endif

            _BugdetService.RemoveBudget(SelectedBudget.Budget);
            Budgets.Remove(SelectedBudget);

            SelectedBudget = null;
        }

#if DEBUG
        Logger.Debug("RemoveBudget complete.");
#endif

        return Task.CompletedTask;
    }
}