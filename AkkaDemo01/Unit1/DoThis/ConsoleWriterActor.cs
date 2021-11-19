using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDemo01.Unit1.DoThis
{
    public class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is Messages.InputError)
            {
                var msg = message as Messages.InputError;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(msg.Reason);
            }
            else if (message is Messages.InputSuccess)
            {
                var msg = message as Messages.InputSuccess;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(msg.Reason);
            }
            else
            {
                Console.WriteLine(message);
            }

            Console.ResetColor();
        }

    }
}
