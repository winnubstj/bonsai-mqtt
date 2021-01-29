using System;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net;
using System.Text;
using System.Diagnostics;

namespace Bonsai.MQTT
{
    public class PublishClient : IDisposable
    {
        private Stopwatch stopwatch = new Stopwatch();
        private MqttClient client;
        private bool verbose;
        /// <summary>
        /// Client for publishing messages to MQTT Broken
        /// </summary>
        /// <param name="broker">Broker ip e.g. "127.0.0.1"</param>
        /// <param name="port">Port integer</param>
        /// <param name="_verbose">Flag for verbose output</param>
        public PublishClient(string broker, int port, bool _verbose)
        {
            // set verbose status.
            verbose = _verbose;
            //stopwatch.Start();
            if (verbose)
            {
                Console.WriteLine($"bonsai-mqtt: Connecting to MQTT on broker {broker}:{port}");
                stopwatch.Start();
            }
            // Connect to mqtt broker.
            #pragma warning disable 618
            client = new MqttClient(IPAddress.Parse(broker), port, false, null, null, MqttSslProtocols.None);
            client.Connect(Guid.NewGuid().ToString());
        }
        /// <summary>
        /// Publish message on MQTT topic
        /// </summary>
        /// <param name="topic">topic e.g.: home/test</param>
        /// <param name="msg">message string. </param>
        public void Publish(string topic, string msg)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
        /// <summary>
        /// Dispose of client and disconnect.
        /// </summary>
        public void Dispose()
        {
            client.Disconnect();
            if (verbose)
            { 
                Console.WriteLine($"bonsai-mqtt: Disconnected");
                stopwatch.Stop();
                Console.WriteLine($"bonsai-mqtt: Elapsed {stopwatch.ElapsedMilliseconds}");
            }
        }
    }
}
