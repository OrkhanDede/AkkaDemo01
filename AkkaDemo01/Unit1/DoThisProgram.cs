using System;
using Akka.Actor;
using AkkaDemo01.Unit1.DoThis;
using AkkaDemo01.Unit1.WinTail;

namespace AkkaDemo01.Unit1
{
    public  class DoThisProgram
    {
        public static ActorSystem MyActorSystem;

        public  void Main()
        {

            MyActorSystem = ActorSystem.Create("MyActorSystem");

            PrintInstructions();
            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create<ConsoleWriterActor>(), "writer");

            // make tailCoordinatorActor
            var tailCoordinatorProps = Props.Create(() => new TailCoordinatorActor());
            var tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps,
                "tailCoordinatorActor");


            var fileValidatorActorProps = Props.Create(() =>
                new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            var validationActor = MyActorSystem.ActorOf(fileValidatorActorProps,
                "validationActor");




            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create<ConsoleReaderActor>(validationActor), "reader");



            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);
            MyActorSystem.WhenTerminated.Wait();
        }


        private static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" red ");
            Console.ResetColor();
            Console.Write(" and others will appear as");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}
