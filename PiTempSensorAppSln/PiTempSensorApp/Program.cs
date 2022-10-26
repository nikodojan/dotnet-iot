using PiTempSensorApp;
using PiTempSensorApp.MongoDb;

SensorReader sensorReader = new SensorReader();
IDataService dataService = new MongoDbService();
double temperature = 100;
double humidity = 110;
int temperatureTolerance = 1;
int humidityTolerance = 5;

while (true)
{
    var data = sensorReader.ReadData();
    if (data.Temperature == null || data.Humidity == null)
        continue;

    if (temperature - data.Temperature >= temperatureTolerance || temperature - data.Temperature <= -1*temperatureTolerance || humidity - data.Humidity >= humidityTolerance || humidity - data.Humidity <= -1*humidityTolerance)
    {
        temperature = (double)data.Temperature;
        humidity = (double)data.Humidity;
        Console.WriteLine($"temp: {temperature} °C\r\nhum: {humidity} %");
        dataService.PostAsync(data);
    }
    
    Thread.Sleep(1000);
}
