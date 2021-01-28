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
    
    public delegate void MsgHandler(SubscribeClient client, MsgReceivedEventArgs e);

    public class MsgReceivedEventArgs : EventArgs
    {
        public string msg { get; set; }
    }
    public class SubscribeClient : IDisposable
    {
        public event MsgHandler Msg;
        public EventHandler<MsgReceivedEventArgs> MsgReceived; // event

        private Stopwatch stopwatch = new Stopwatch();
        private MqttClient client;
        public SubscribeClient(string broker, int port, string topic)
        {
            //stopwatch.Start();
            Console.WriteLine($"bonsai-mqtt: Connect to MQTT on broker {broker}:{port}");
            // Connect to mqtt broker.
            #pragma warning disable 618
            client = new MqttClient(IPAddress.Parse(broker), port, false, null, null, MqttSslProtocols.None);
            client.Connect(Guid.NewGuid().ToString());
            // subscribe to topic
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            
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
