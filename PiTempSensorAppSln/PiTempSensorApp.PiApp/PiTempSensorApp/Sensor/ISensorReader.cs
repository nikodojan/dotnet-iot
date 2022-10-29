using PiTempSensorApp.Reader.Models;

namespace PiTempSensorApp.Reader.Sensor;

internal interface ISensorReader
{
    EnvironmentData ReadData();
}