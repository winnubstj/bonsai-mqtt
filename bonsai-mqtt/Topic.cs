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
using System.Reactive.Disposables;

namespace Bonsai.MQTT
{
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Subscribe to messages on MQTT topic")]

    public class Topic : Source<System.Reactive.EventPattern<MsgReceivedEventArgs>>
    {
        private SubscribeClient client;
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to subscribe to")]
        public string topic { get; set; } = "test/";

        public override IObservable<System.Reactive.EventPattern<MsgReceivedEventArgs>> Generate()
        {
            client = new SubscribeClient(broker, port, topic);
            return Observable.FromEventPattern<MsgReceivedEventArgs>(h => client.MsgReceived += h, h => {client.MsgReceived -= h; client.Dispose(); });
        }

    }
}

