using System;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;

namespace Bonsai.MQTT
{
    

    public class MsgReceivedEventArgs : EventArgs
    {
        public string msg { get; set; }
    }
    public class SubscribeClient : IDisposable
    {
        public EventHandler<string> MsgReceived; // event.
        public string msg; // message.
        private bool verbose=false;
        private MqttClient client;

        /// <summary>
        /// Client for subscribing to mqtt topic.
        /// </summary>
        /// <param name="broker">Broker ip e.g. "127.0.0.1"</param>
        /// <param name="port">Port integer</param>
        /// <param name="topic">Topic e.g.: home/test</param>
        /// <param name="_verbose">Flag for verbose output</param>
        public SubscribeClient(string broker, int port, string topic, bool _verbose)
        {
            verbose = _verbose;
            if (verbose) { Console.WriteLine($"bonsai-mqtt: Connecting to MQTT on broker {broker}:{port}"); }
            // Connect to mqtt broker.
            #pragma warning disable 618
            client = new MqttClient(IPAddress.Parse(broker), port, false, null, null, MqttSslProtocols.None);
            client.Connect(Guid.NewGuid().ToString());
            // subscribe to topic
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            if (verbose) { Console.WriteLine($"bonsai-mqtt: Subscribed to topic: {topic}"); }
            // register to message received
            client.MqttMsgPublishReceived += MqttReceived;
        }

        /// <summary>
        /// Called on message received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MqttReceived(object sender, MqttMsgPublishEventArgs e)
        {
            msg = System.Text.Encoding.Default.GetString(e.Message);
            MsgReceived?.Invoke(this, msg);
        }
        /// <summary>
        /// Dispose of client and disconnect.
        /// </summary>
        public void Dispose()
        {
            client.Disconnect();
            if (verbose) { Console.WriteLine($"bonsai-mqtt: Disconnected"); }
        }
    }
}
