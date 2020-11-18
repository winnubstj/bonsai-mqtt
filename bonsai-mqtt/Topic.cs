using System;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Drawing.Design;
using System.Collections.Generic;

namespace Bonsai.MQTT
{
    [Description("Receive messages on mqtt topic")]
    public class Topic : Source<int>
    {
        public override IObservable<int> Generate()
        {
            //Console.WriteLine("test");
            return Observable.Return(0);
        }
    }
}

