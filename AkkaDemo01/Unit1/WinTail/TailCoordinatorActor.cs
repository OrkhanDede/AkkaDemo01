using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDemo01.Unit1.WinTail
{
    public class TailCoordinatorActor : UntypedActor
    {
        #region Message Types

        public class StartTail
        {
            public StartTail(string filePath, IActorRef reportActor)
            {
                FilePath = filePath;
                ReportActor = reportActor;
            }
            public string FilePath { get; private set; }
            public IActorRef ReportActor { get; private set; }
        }
        public class StopTail
        {
            public StopTail(string filePath)
            {
                FilePath = filePath;
            }
            public string FilePath { get; private set; }
        }
        #endregion


        protected override void OnReceive(object message)
        {
            if (message is StartTail msg)
            {
                //YOU NEED TO FILL IN HERE
                Context.ActorOf(Props.Create(() => new TailActor(msg.FilePath, msg.ReportActor)), "tail-actor");
            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                10, // maxNumberOfRetries
                TimeSpan.FromSeconds(30), // withinTimeRange
                x => // localOnlyDecider
                {
                    //Maybe we consider ArithmeticException to not be application critical
                    //so we just ignore the error and keep going.
                    if (x is ArithmeticException) return Directive.Resume;

                    //Error that we cannot recover from, stop the failing actor
                    else if (x is NotSupportedException) return Directive.Stop;

                    //In all other cases, just restart the failing actor
                    else return Directive.Restart;
                });
        }

    }
}
