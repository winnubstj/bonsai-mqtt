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

namespace Bonsai.MQTT
{
    [Description("Publish messages on MQTT topic")]
    public class Publish:Sink<string>
    {
        private MqttClient client;
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to send message on")]
        public string topic { get; set; } = "test/";

        public override IObservable<string> Process(IObservable<string> source)
        {
            Connect();
            return source.Select(input =>
            {
                PublishMsg(input);
                return input;
            });
        }
        private void Connect()
        {
            Console.WriteLine($"Connect to MQTT on broker {broker}:{port}");
            // Connect to mqtt broker.
            #pragma warning disable 618
            client = new MqttClient(IPAddress.Parse(broker),port, false, null, null, MqttSslProtocols.None);
            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);
        }
        private void PublishMsg(string msg)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(msg), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }
    }
}
