using System;
using System.Reactive.Linq;
using System.ComponentModel;

namespace Bonsai.MQTT
{
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Subscribe to messages on MQTT topic")]

    public class SubscribeToMQTT : Source<System.Reactive.EventPattern<string>>
    {
        private SubscribeClient client;
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to subscribe to")]
        public string topic { get; set; } = "test/";
        [Description("Verbose status messages")]
        public bool verbose { get; set; } = false;

        public override IObservable<System.Reactive.EventPattern<string>> Generate()
        {
            client = new SubscribeClient(broker, port, topic, verbose);
            return Observable.FromEventPattern<string>(h => client.MsgReceived += h, h => {client.MsgReceived -= h; client.Dispose(); });
        }

    }
}

