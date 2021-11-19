using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
namespace AkkaDemo01.Unit1.DoThis
{
    public static class DoThisProgram
    {
        public static ActorSystem MyActorSystem;

        public static void Main()
        {

            MyActorSystem = ActorSystem.Create("MyActorSystem");

            PrintInstructions();
            

            var consoleWriterActor = MyActorSystem.ActorOf(Props.Create<ConsoleWriterActor>(), "writer");

            var validatorActor = MyActorSystem.ActorOf(Props.Create<ValidationActor>(consoleWriterActor),"validation");

            var consoleReaderActor = MyActorSystem.ActorOf(Props.Create<ConsoleReaderActor>(validatorActor),"reader");



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
