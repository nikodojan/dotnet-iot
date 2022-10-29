using PiTempSensorApp.Reader.Models;

namespace PiTempSensorApp.Reader.Services;

internal interface IDataService
{
    Task SendAsync(EnvironmentData data);
}
