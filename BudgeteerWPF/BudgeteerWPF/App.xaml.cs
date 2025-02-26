using BudgeteerWPF.ViewModels;
using BudgetManagement;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Windows;
using LogLevel = NLog.LogLevel;

namespace BudgeteerWPF;

/// <summary>
/// Interaction logic for App.xaml
/// 
/// to do:
/// 
///     implement value validation (dates, avoid negative values, give warning when budget is exceeded)
///          
///     implement flow document output
/// 
///     further implement logging with #If DEBUG instructions
/// 
///     implement documentation
///     
///     ((implement some ui features))
///     
///     implement unit tests
///     
///     make clean mvvm, move the messagebox.show for remove budget from the viewmodel to the view,
///     use delegate command to invoke logic upon click, implement on budgetChange as well
/// </summary>
public partial class App : Application
{

#if DEBUG
    // DEBUG logger
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
#endif

    ServiceProvider provider;
    ServiceCollection services = new ServiceCollection();


    // no need, but i was curious. now again i know, it doesn't show.
    //    public App()
    //    {
    //#if DEBUG
    //        Logger.Debug("App() constructor init.");
    //#endif

    //#if DEBUG
    //        Logger.Debug("App() constructor end.");
    //#endif
    //    }


    /// <summary>
    /// awaits BudgetService.SaveBudgetServiceData() before exit
    /// </summary>
    /// <param name="e"></param>
    protected override async void OnExit(ExitEventArgs e)
    {

#if DEBUG
        Logger.Debug("OnExit start.");
#endif


        BudgetService budgetService = provider.GetRequiredService<BudgetService>();


        await budgetService.SaveBudgetServiceData();


#if DEBUG
        Logger.Debug("OnExit complete.");
#endif


        base.OnExit(e);

    }


    /// <summary>
    /// instantiates required classes via dependency injection
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {

#if DEBUG
        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "App_${shortdate}.txt");
        });

        services.AddLogging((o) => LogManager.GetCurrentClassLogger());

        Logger.Debug("OnStartup start.");
#endif

        services.AddSingleton<MainWindow>();

        services.AddSingleton<BudgetService>();

        services.AddSingleton<BudgetServiceViewModel>();

        services.AddSingleton<MainViewModel>();


#if DEBUG
        Logger.Debug("OnStartup building provider..");
#endif

        provider = services.BuildServiceProvider();


        MainWindow mainWindow = provider.GetRequiredService<MainWindow>();

        mainWindow.DataContext = provider.GetRequiredService<MainViewModel>();

#if DEBUG
        Logger.Debug("OnStartup almost complete..");
#endif

        mainWindow.Show();

#if DEBUG
        Logger.Debug("MainWindow shown. OnStartup complete.");
#endif


        base.OnStartup(e);
    }

}