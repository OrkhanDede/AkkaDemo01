﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaDemo01.Unit1.DoThis
{
    public class ValidationActor:UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;
        public ValidationActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }
        protected override void OnReceive(object message)
        {
            var msg = message as string;
            GetAndValidateInput(msg);
        }

        /// <summary>
        /// Reads input from console, validates it, then signals appropriate response
        /// (continue processing, error, success, etc.).
        /// </summary>

        private void GetAndValidateInput(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                _consoleWriterActor.Tell(
                    new Messages.NullInputError("No input received."));
            }
            else
            {
                var valid = IsValid(message);
                if (valid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid!"));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError("Invalid input had odd number of characters"));
                }
            }
            Sender.Tell(new Messages.ContinueProcessing());
        }

        /// <summary>
        /// Validates <see cref="message"/>.
        /// Currently says messages are valid if contain even number of characters.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static bool IsValid(string message)
        {
            var valid = message.Length % 2 == 0;
            return valid;
        }
    }
}
