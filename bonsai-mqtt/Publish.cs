using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.Collections.Generic;

namespace Bonsai.MQTT
{
    [Description("Publish messages on MQTT topic")]
    public class Publish:Sink<string>
    {
        // Settings
        [Description("IP of MQTT message broker")]
        public string broker { get; set; } = "127.0.0.1";
        public Publish()
        {
            Console.WriteLine("start up!");
        }
        public override IObservable<string> Process(IObservable<string> source)
        {
            return source.Select(input =>
            {
                Console.WriteLine(input);
                return input;

            });
        }
    }
}
