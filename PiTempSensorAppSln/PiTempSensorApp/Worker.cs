using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PiTempSensorApp.Models;
using PiTempSensorApp.Sensor;
using PiTempSensorApp.Services;

namespace PiTempSensorApp
{
    internal class Worker : IWorker
    {
        SensorReader _sensorReader;
        double _temperature = 100;
        double _humidity = 110;
        int _temperatureTolerance = 1;
        int _humidityTolerance = 5;
        private IEnumerable<IDataService> _services;

        public Worker(IConfiguration config, IServiceProvider services)
        {
            _services = services.GetServices<IDataService>();
            _temperatureTolerance = config.GetValue<int>("Tolerances:TemperatureTolerance");
            _humidityTolerance = config.GetValue<int>("Tolerances:HumidityTolerance");
            _sensorReader = new SensorReader();
        }

        public void Run()
        {
            while (true)
            {
                var data = _sensorReader.ReadData();
                if (data.Temperature == null || data.Humidity == null)
                    continue;

                if (_temperature - data.Temperature >= _temperatureTolerance || _temperature - data.Temperature <= -1 * _temperatureTolerance || _humidity - data.Humidity >= _humidityTolerance || _humidity - data.Humidity <= -1 * _humidityTolerance)
                {
                    _temperature = (double)data.Temperature;
                    _humidity = (double)data.Humidity;
                    Console.WriteLine($"temp: {_temperature} °C\r\nhum: {_humidity} %");
                    SendData(data);
                }

                Thread.Sleep(1000);
            }
        }

        private async Task SendData(EnvironmentData data)
        {
            foreach (var dataService in _services)
            {
               await dataService.SendAsync(data);
            }
        }
    }
}
