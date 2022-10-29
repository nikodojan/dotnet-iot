using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PiTempSensorApp.Models;

namespace PiTempSensorApp.Services;

internal class UdpService : IDataService, IDisposable
{
    private readonly UdpClient _udp;
    private readonly IPEndPoint _broadcastEndPoint;

    public UdpService(IConfiguration config)
    {
        int port = config.GetValue<int>("UdpPort");
        _udp = new UdpClient();
        _broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, port);
    }

    public async Task SendAsync(EnvironmentData data)
    {
        var message = JsonSerializer.Serialize(data);
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await _udp.SendAsync(messageBytes, messageBytes.Length, _broadcastEndPoint);
    }

    public void Dispose()
    {
        _udp.Dispose();
    }
}