using PiTempSensorApp.Reader.Shared.Models;

namespace PiTempSensorApp.Reader.Shared.DataServices;

internal interface IDataService
{
    Task SendAsync(EnvironmentData data);
}
