using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDemo01.Unit1.Utils;

namespace AkkaDemo01.Unit1.WinTail
{
    public class TailActor : UntypedActor
    {


        #region Message Types

        public class FileWrite
        {
            public FileWrite(string fileName)
            {
                FileName = fileName;
            }
            public string FileName { get; private set; }
        }
        public class FileError
        {
            public FileError(string fileName, string reason)
            {
                FileName = fileName;
                Reason = reason;
            }
            public string FileName { get; private set; }
            public string Reason { get; private set; }
        }
        public class InitialRead
        {
            public InitialRead(string fileName, string text)
            {
                FileName = fileName;
                Text = text;
            }
            public string FileName { get; private set; }
            public string Text { get; private set; }
        }

        #endregion

        private readonly string _filePath;
        private readonly IActorRef _reporterActor;
        private readonly FileObserver _observer;
        private readonly Stream _fileStream;
        private readonly StreamReader _fileStreamReader;

        public TailActor(string filePath, IActorRef reporterActor)
        {
            _filePath = filePath;
            _reporterActor = reporterActor;
            // start watching file for changes
            _observer = new FileObserver(Self, Path.GetFullPath(_filePath));
            _observer.Start();


            _fileStream = new FileStream(Path.GetFullPath(_filePath),
                FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _fileStreamReader = new StreamReader(_fileStream, Encoding.UTF8);

            // read the initial contents of the file and send it to console as first msg
            var text = _fileStreamReader.ReadToEnd();

            Self.Tell(new InitialRead(_filePath, text));

        }

        protected override void OnReceive(object message)
        {
            if (message is FileWrite)
            {

                // move file cursor forward
                // pull results from cursor to end of file and write to output
                // (this is assuming a log file type format that is append-only)

                var text = _fileStreamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(text))
                {
                    _reporterActor.Tell(text);
                }

            }
            else if (message is FileError fe)
            {
                _reporterActor.Tell(string.Format($"Tail error {fe.Reason}"));

            }
            else if (message is InitialRead ir)
            {
                _reporterActor.Tell(ir.Text);
            }
        }
    }
}
