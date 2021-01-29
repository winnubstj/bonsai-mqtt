using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using System.Diagnostics;

namespace Bonsai.MQTT
{
    

    public class MsgReceivedEventArgs : EventArgs
    {
        public string msg { get; set; }
    }
    public class SubscribeClient : IDisposable
    {
        public EventHandler<MsgReceivedEventArgs> MsgReceived; // event
        public MsgReceivedEventArgs msgEvent = new MsgReceivedEventArgs();

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
            Console.WriteLine(topic);
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            // register to message received
            client.MqttMsgPublishReceived += MqttReceived;

        }

        void MqttReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("received");
            string msg = System.Text.Encoding.Default.GetString(e.Message);
            msgEvent.msg = msg;
            MsgReceived?.Invoke(this, msgEvent);
        }
        public void Dispose()
        {
            Console.WriteLine($"bonsai-mqtt: disconnecting");
            client.Disconnect();
        }
    }
}
