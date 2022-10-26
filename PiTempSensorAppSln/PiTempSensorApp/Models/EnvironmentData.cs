using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiTempSensorApp.Models
{
    internal class EnvironmentData
    {

        public EnvironmentData()
        {

        }

        public EnvironmentData(double temperature, double humidity, DateTime dateTime)
        {
            Temperature = temperature;
            Humidity = humidity;
        }

        public double? Temperature { get; set; } = null;
        public double? Humidity { get; set; } = null;
        public DateTime DateTime { get; set; }
    }
}
