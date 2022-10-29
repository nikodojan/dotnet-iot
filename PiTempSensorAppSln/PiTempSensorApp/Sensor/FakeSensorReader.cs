using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiTempSensorApp.Models;

namespace PiTempSensorApp.Sensor
{
    internal class FakeSensorReader : ISensorReader
    {
        Random _random = new Random();
        EnvironmentData _data = new EnvironmentData();

        public EnvironmentData ReadData()
        {
            _data.Temperature = _random.Next(18,25);
            _data.Humidity = _random.Next(70, 95);
            _data.DateTime = DateTime.Now;
            return _data;
        }
    }
}
