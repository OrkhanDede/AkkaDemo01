using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Unit2DataMontoring.Actors
{
    public class Messages
    {
        #region Reporting

        /// <summary>
        /// Signal used to indicate that it's time to sample all counters
        /// </summary>
        public class GatherMetrics { }
      

        public class Metric
        {
            public string Series { get; private set; }
            public float CounterValue { get; private set; }

            public Metric(string series, float counterValue)
            {
                Series = series;
                CounterValue = counterValue;
            }
        }
        #endregion

        #region Performance counter managment

        public enum CounterType
        {
            Cpu,
            Memory,
            Disk
        }
        public class SubscribeCounter
        {
            public SubscribeCounter(CounterType counterType, IActorRef subscriber)
            {
                CounterType = counterType;
                Subscriber = subscriber;
            }
            public CounterType CounterType { get; private set; }
            public IActorRef Subscriber { get; private set; }
        }
        public class UnsubscribeCounter
        {
            public UnsubscribeCounter(CounterType counterType, IActorRef subscriber)
            {
                CounterType = counterType;
                Subscriber = subscriber;
            }
            public CounterType CounterType { get; private set; }
            public IActorRef Subscriber { get; private set; }
        }

        #endregion
    }
}
