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
        private readonly IActorRef _consoleValidatorActor;
        private const string ExitCommand = "exit";
        public const string StartCommand = "start";

        public ConsoleReaderActor(IActorRef consoleValidatorActor)
        {
            _consoleValidatorActor = consoleValidatorActor;
        }
        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            GetAndValidateInput();
        }
        #region Internal methods
        private void DoPrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }

        /// <summary>
        /// Reads input from console, validates it, then signals appropriate response
        /// (continue processing, error, success, etc.).
        /// </summary>

        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (string.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }
            _consoleValidatorActor.Tell(message);
        }
        #endregion
    }
}
