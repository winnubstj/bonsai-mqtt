using System;
using System.Reactive.Linq;
using System.ComponentModel;

namespace Bonsai.MQTT
{
    [Description("Publish messages on MQTT topic")]
    public class PublishToMQTT : Sink<string>
    {
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to send message on")]
        public string topic { get; set; } = "test/";
        [Description("Verbose status messages")]
        public bool verbose { get; set; } = false;

        public override IObservable<string> Process(IObservable<string> source)
        {
                return source.Select(input =>
                {
                    using (PublishClient client = new PublishClient(broker, port, verbose))
                    {
                        client.Publish(topic, input);
                        return input;
                    }
                });

        }

    }
}
