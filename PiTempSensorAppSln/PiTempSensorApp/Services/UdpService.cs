using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PiTempSensorApp.Models;

namespace PiTempSensorApp.Services;

internal class UdpService : IDataService
{
    private readonly UdpClient _udp;
    private readonly int _port;

    public UdpService(IConfiguration config)
    {
        _udp = new UdpClient(port: _port);
        _port = config.GetValue<int>("UdpPort");
    }
    public async Task SendAsync(EnvironmentData data)
    {
        var message = JsonSerializer.Serialize(data);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await _udp.SendAsync(messageBytes);
    }
}