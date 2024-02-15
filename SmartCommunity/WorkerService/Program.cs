using NLog;
using NLog.Web;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .Build();
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
logger.Info("Start Smart Community Consumer worker ...");

await host.RunAsync();