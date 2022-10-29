using PiTempSensorApp.Models;

namespace PiTempSensorApp.Sensor;

internal interface ISensorReader
{
    EnvironmentData ReadData();
}