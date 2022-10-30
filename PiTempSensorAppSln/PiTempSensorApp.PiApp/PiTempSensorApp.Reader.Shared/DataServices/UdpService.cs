using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PiTempSensorApp.Reader.Shared.Models;

namespace PiTempSensorApp.Reader.Shared.DataServices;

internal class UdpService : IDataService, IDisposable
{
    private readonly UdpClient _udp;
    private readonly IPEndPoint _broadcastEndPoint;

    public UdpService(IConfiguration config)
    {
        var port = Convert.ToInt32(config.GetSection("UdpPort").Value);
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