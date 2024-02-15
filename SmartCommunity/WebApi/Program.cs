using NLog;
using NLog.Web;
using Supabase;
using Supabase.Gotrue;
using WebApi.Repository;
using WebApi.Repository.Interface;
using WebApi.Service;
using WebApi.Service.Interface;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try {
    var builder = WebApplication.CreateBuilder(args);
    
    // config
    logger.Info("Start Smart Community Web API ...");
    //將NLog註冊到此專案內
    builder.Logging.ClearProviders();
    //設定log紀錄的最小等級
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var url = builder.Configuration.GetValue<string>("Settings:SupabaseSettings:SupabaseUrl");
    var key = builder.Configuration.GetValue<string>("Settings:SupabaseSettings:SupabaseKey");
    var options = new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true,
        // SessionHandler = new SupabaseSessionHandler() <-- This must be implemented by the developer
    };

    // Note the creation as a singleton.
    builder.Services.AddSingleton(provider => new Supabase.Client(url, key, options));
    builder.Services.AddScoped<ISupaBaseRepository, SupaBaseRepository>();
    builder.Services.AddScoped<ISupaBaseService, SupaBaseService>();
    builder.Services.AddScoped<IMessageProducerService, MessageProducerService>();
    

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    // if (app.Environment.IsDevelopment()) {
    //     app.UseSwagger();
    //     app.UseSwaggerUI();
    // }
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
    );

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
} catch (Exception ex) {
    // 捕獲設定錯誤的錯誤紀錄
    logger.Error(ex, "Stopped program because of exception");
    throw;
} finally {
    //須確定在關閉時，把nlog關閉
    LogManager.Shutdown();
}