using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PiTempSensorApp.Models;

namespace PiTempSensorApp;

internal interface IDataService
{
    Task PostAsync(EnvironmentData data);
}
