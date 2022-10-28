using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiTempSensorApp.MongoDb;
using PiTempSensorApp.Sensor;
using PiTempSensorApp.Services;

namespace PiTempSensorApp
{
    internal class Worker : IWorker
    {
        SensorReader sensorReader = new SensorReader();
        IDataService dataService = new MongoDbService();
        double temperature = 100;
        double humidity = 110;
        int temperatureTolerance = 1;
        int humidityTolerance = 5;

        public void Run()
        {
            while (true)
            {
                var data = sensorReader.ReadData();
                if (data.Temperature == null || data.Humidity == null)
                    continue;

                if (temperature - data.Temperature >= temperatureTolerance || temperature - data.Temperature <= -1 * temperatureTolerance || humidity - data.Humidity >= humidityTolerance || humidity - data.Humidity <= -1 * humidityTolerance)
                {
                    temperature = (double)data.Temperature;
                    humidity = (double)data.Humidity;
                    Console.WriteLine($"temp: {temperature} °C\r\nhum: {humidity} %");
                    dataService.PostAsync(data);
                }

                Thread.Sleep(1000);
            }
        }
    }
}
