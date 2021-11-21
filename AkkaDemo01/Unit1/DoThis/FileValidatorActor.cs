using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDemo01.Unit1.WinTail;

namespace AkkaDemo01.Unit1.DoThis
{
    public class FileValidatorActor:UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;
        private readonly IActorRef _tailCoordinatorActor;
        public FileValidatorActor(IActorRef consoleWriterActor, IActorRef tailCoordinatorActor)
        {
            _consoleWriterActor = consoleWriterActor;
            _tailCoordinatorActor = tailCoordinatorActor;
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
                    new Messages.NullInputError("Input was blank.Please try again.\n"));
                Sender.Tell(new Messages.ContinueProcessing());

            }
            else
            {
                var valid = IsFileUri(message);
                if (valid)
                {
                    // signal successful input
                    _consoleWriterActor.Tell(new Messages.InputSuccess($"Starting processing for {message}"));
                    // start coordinator
                    _tailCoordinatorActor.Tell(new TailCoordinatorActor.StartTail(message,
                        _consoleWriterActor));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError($"{message} is not an existing URI on disk"));
                    Sender.Tell(new Messages.ContinueProcessing());
                }
            }
        }

        /// <summary>
        /// Checks if file exists at path provided by user.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsFileUri(string path)
        {
            return File.Exists(path);
        }
    }
}
