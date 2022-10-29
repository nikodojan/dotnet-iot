using Iot.Device.DHTxx;
using PiTempSensorApp.Models;

namespace PiTempSensorApp.Sensor;
internal class Dht22SensorReader : ISensorReader
{
    private Dht22 _dht;
    public Dht22SensorReader()
    {
        _dht = new Dht22(7, System.Device.Gpio.PinNumberingScheme.Board);
    }

    public EnvironmentData ReadData()
    {
        var envData = new EnvironmentData();

        if (_dht.TryReadTemperature(out var temperature))
            envData.Temperature = Math.Round(temperature.DegreesCelsius, 1);

        if (_dht.TryReadHumidity(out var humidity))
            envData.Humidity = Math.Round(humidity.Percent, 1);

        envData.DateTime = DateTime.Now.ToLocalTime();

        return envData;
    }
}

