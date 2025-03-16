using BudgeteerWPF.ViewModels;
using BudgetManagement;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using LogLevel = NLog.LogLevel;

namespace BudgeteerWPF;

/// <summary>
/// Interaction logic for App.xaml
/// 
/// to do:
/// 
///     keep enum value on add budget change
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
    // interessant, aber potenziell nervig, falls es eine app mit gleicher Signatur gibt, z.b.
    // weil die von hier kopiert wurde, kann es nicht mehr starten.
    private static Mutex _InstanceMutex;
    private static string _ApplicationKey =
        @"Budgeteer {15839914-5721-46DC-999F-EDE342F9450F}";// online generated uuid "cf645c2c-b646-4c2e-be76-6b15f1a4f6d3"

    // DEBUG logger
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

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
        // c# feature test: dynamic keyword & expando object
        //dynamic obj = new ExpandoObject();
        //obj.Text = "Hallo Welt";
        //obj.Value = 115.17m;
        //obj.Increment = (Action)(() => obj.Value++);

        //Logger.Debug(obj);
        //Logger.Debug(obj.Text);
        //Logger.Debug(obj.Value);

        //obj.Increment();
        
        //Logger.Debug(obj.Value);

#if DEBUG
        Logger.Debug("OnExit start.");
#endif

        BudgetService budgetService = provider.GetRequiredService<BudgetService>();
                
        await budgetService.SaveBudgetServiceData();        

#if DEBUG
        Logger.Debug("OnExit complete.");
#endif

        base.OnExit(e);

        KillInstance(e.ApplicationExitCode);

    }

    /// <summary>
    /// instantiates required classes via dependency injection
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStartup(StartupEventArgs e)
    {
        SplashScreen splashScreen = new SplashScreen();
        splashScreen.Show();

        NLog.LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToConsole();
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToFile(fileName: "App_${shortdate}.txt");
        });

        services.AddLogging((o) => LogManager.GetCurrentClassLogger());

#if DEBUG
        Logger.Debug("OnStartup start.");
#endif

        if (StartInstance())
        {
            Logger.Debug("There is already an active instance.");

            Application.Current.Shutdown(1);
        }

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

        splashScreen.Close();
        mainWindow.Show();

#if DEBUG
        Logger.Debug("MainWindow shown. OnStartup complete.");
#endif


        base.OnStartup(e);
    }

    public static bool StartInstance()
    {
        _InstanceMutex = new Mutex(true, _ApplicationKey);

        bool _InstanceMutexIsInUse = false;

        try
        {
            _InstanceMutexIsInUse = !_InstanceMutex.WaitOne(TimeSpan.Zero, true);
        }
        catch (AbandonedMutexException)
        {
            KillInstance();
            _InstanceMutexIsInUse = false;
        }
        catch (Exception)
        {
            _InstanceMutex.Close();
            _InstanceMutexIsInUse = false;
        }

        return _InstanceMutexIsInUse;
    }

    public static void KillInstance(int code = 0)
    {
        if (_InstanceMutex is null) return;

        if (code == 0)
        {
            try
            {
                _InstanceMutex.ReleaseMutex();
            }
            catch (Exception)
            {

            }

            _InstanceMutex.Close();
        }
    }

}