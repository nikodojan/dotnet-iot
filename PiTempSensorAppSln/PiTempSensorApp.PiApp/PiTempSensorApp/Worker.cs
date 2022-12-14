using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PiTempSensorApp.Reader.Models;
using PiTempSensorApp.Reader.Sensor;
using PiTempSensorApp.Reader.Services;

namespace PiTempSensorApp.Reader;

internal class Worker : IWorker
{
    private readonly ISensorReader _sensorReader;
    double _oldTemperature = 100;
    double _oldHumidity = 110;
    private readonly int _temperatureTolerance = 1;
    private readonly int _humidityTolerance = 5;
    private readonly IEnumerable<IDataService> _dataServices;
    private readonly ILogger<Worker> _logger;

    public Worker(
        IConfiguration config, 
        IServiceProvider services, 
        ILogger<Worker> logger, 
        ISensorReader sensorReader)
    {
        _dataServices = services.GetServices<IDataService>();
        _temperatureTolerance = config.GetValue<int>("Tolerances:TemperatureTolerance");
        _humidityTolerance = config.GetValue<int>("Tolerances:HumidityTolerance");
        _logger = logger;
        _sensorReader = sensorReader;
    }

    public async Task Run()
    {
        while (true)
        {
            try
            {
                var data = _sensorReader.ReadData();
                if (data.Temperature == null || data.Humidity == null)
                    continue;

                if (DataHasChanged(data))
                {
                    SetOldValues(data);
                    Console.WriteLine($"temp: {_oldTemperature} °C\r\nhum: {_oldHumidity} %");
                    await SendData(data);
                }

                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }

        }
    }

    private void SetOldValues(EnvironmentData data)
    {
        _oldTemperature = data.Temperature ?? _oldTemperature;
        _oldHumidity = data.Humidity ?? _oldHumidity;
    }

    private bool DataHasChanged(EnvironmentData data)
    {
        return _oldTemperature - data.Temperature >= _temperatureTolerance ||
               _oldTemperature - data.Temperature <= -1 * _temperatureTolerance ||
               _oldHumidity - data.Humidity >= _humidityTolerance ||
               _oldHumidity - data.Humidity <= -1 * _humidityTolerance;
    }

    private async Task SendData(EnvironmentData data)
    {
        foreach (var dataService in _dataServices)
        {
            await dataService.SendAsync(data);
        }
    }
}