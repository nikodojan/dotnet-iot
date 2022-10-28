using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiTempSensorApp.MongoDb;
using Microsoft.Extensions.Hosting;
using PiTempSensorApp;
using PiTempSensorApp.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<IWorker, Worker>();
    })
    .Build();


