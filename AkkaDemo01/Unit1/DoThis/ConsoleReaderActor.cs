using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDemo01.Unit1.DoThis
{
    public class ConsoleReaderActor : UntypedActor
    {
        private IActorRef _consoleWriteActor;
        private const string ExitCommand = "exit";

        public ConsoleReaderActor(IActorRef consoleWriteActor)
        {
            _consoleWriteActor = consoleWriteActor;
        }
        protected override void OnReceive(object message)
        {
            var read = Console.ReadLine();
            if (!string.IsNullOrEmpty(read) &&
                string.Equals(read, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // shut down the system (acquire handle to system via
                // this actors context)
                Context.System.Terminate();
                return;
            }
            _consoleWriteActor.Tell(read);
            Self.Tell("continue");
        }
    }
}
