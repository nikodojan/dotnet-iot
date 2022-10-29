using PiTempSensorApp.Reader.Models;

namespace PiTempSensorApp.Reader.Sensor
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
