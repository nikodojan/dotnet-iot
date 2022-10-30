using Iot.Device.DHTxx;
using PiTempSensorApp.Reader.Shared.Models;

namespace PiTempSensorApp.Reader.Shared.Sensor;
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
            envData.Temperature = Math.Round((double)temperature.DegreesCelsius, 1);

        if (_dht.TryReadHumidity(out var humidity))
            envData.Humidity = Math.Round((double)humidity.Percent, 1);

        envData.DateTime = DateTime.Now.ToLocalTime();

        return envData;
    }
}

