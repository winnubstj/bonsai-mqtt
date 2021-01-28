using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Bonsai.MQTT
{
    public class Client : IDisposable
    {
        private Stopwatch stopwatch = new Stopwatch();
        private MqttClient client;
        public Client(string broker, int port)
        {
            //stopwatch.Start();
            Console.WriteLine($"bonsai-mqtt: Connect to MQTT on broker {broker}:{port}");
            // Connect to mqtt broker.
            #pragma warning disable 618
            client = new MqttClient(IPAddress.Parse(broker), port, false, null, null, MqttSslProtocols.None);
            client.Connect(Guid.NewGuid().ToString());
        }
        public void Publish(string topic, string msg)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
        public void Dispose()
        {
            Console.WriteLine($"bonsai-mqtt: disconnecting");
            client.Disconnect();
            //stopwatch.Stop();
            //Console.WriteLine($"elapsed: {stopwatch.ElapsedMilliseconds}");
            //stopwatch.Reset();
        }
    }
}
