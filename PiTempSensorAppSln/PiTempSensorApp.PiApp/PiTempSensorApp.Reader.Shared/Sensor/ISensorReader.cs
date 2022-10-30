using PiTempSensorApp.Reader.Shared.Models;

namespace PiTempSensorApp.Reader.Shared.Sensor;

internal interface ISensorReader
{
    EnvironmentData ReadData();
}