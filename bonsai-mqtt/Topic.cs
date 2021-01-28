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
    [WorkflowElementCategory(ElementCategory.Source)]
    [Description("Subscribe to messages on MQTT topic")]

    public class Topic : Source<string>
    {
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        [Description("port of MQTT message broker")]
        public int port { get; set; } = 1883;
        [Description("Topic to subscribe to")]
        public string topic { get; set; } = "test/";

        public override IObservable<string> Generate()
        {
            return Observable.Create<string>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(() =>
                {
                    observer.OnNext("test");
                    while (!cancellationToken.IsCancellationRequested)
                    {
                    }
                });
            }

            );
/*            return Observable.Create<string>((observer, cancellationToken) =>
            {
                return Task.Factory.StartNew(() =>
                {
                    using (var receiver = new PullSocket(string.Format("@tcp://{0}:{1}", Address, Port)))
                    {
                        observer.OnNext(receiver.ReceiveFrameString());
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            string msg = receiver.ReceiveFrameString();
                            if (msg != null)
                            {
                                observer.OnNext(msg);
                                Console.WriteLine("From Client: {0}", msg);
                            }
                        }
                    }
                });
            })
            .PublishReconnectable()
            .RefCount();*/
        }
    }
}

