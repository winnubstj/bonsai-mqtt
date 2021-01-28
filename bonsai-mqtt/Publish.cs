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
    public class Publish : Sink<string>
    {
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to send message on")]
        public string topic { get; set; } = "test/";

        public override IObservable<string> Process(IObservable<string> source)
        {
            return source.Select(input =>
            {
                using (Client client = new Client(broker, port))
                {
                    client.Publish(topic, input);
                    return input;
                }
            });
        }
    }
}
