﻿using System.Drawing.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PiTempSensorApp;
using PiTempSensorApp.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((hostBuilder, services) =>
    {
        ConfigureDataServices(hostBuilder.Configuration, services);
        services.AddSingleton<IWorker, Worker>();
    })
    .Build();


var worker = host.Services.GetRequiredService<IWorker>();
worker.Run();

void ConfigureDataServices(IConfiguration config, IServiceCollection services)
{
    var requestedServices = config.GetSection("DataServices").Get<string[]>();
    if (!requestedServices.Any())
        throw new ArgumentNullException("No data services provided in the configuration.");

    foreach (var requestedService in requestedServices)
    {
        switch (requestedService)
        {
            case "MongoDb":
                services.AddTransient<IDataService, MongoDbService>();
                break;
            case "Udp":
                services.AddTransient<IDataService, UdpService>();
                break;
        }
    }
}