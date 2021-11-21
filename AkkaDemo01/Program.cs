using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaDemo01.Actors.Demo;
using AkkaDemo01.Actors.Game;
using AkkaDemo01.Actors.Game.Messages;
using AkkaDemo01.Actors.Game.Model;
using AkkaDemo01.Unit1;
using AkkaDemo01.Unit1.DoThis;

namespace AkkaDemo01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DoThisProgram.Main();
        }

        #region MutliThread sample

        static void BankAccountSample()
        {
            var bankProgram = new BankProgram();
            bankProgram.Run().Wait();
            Console.ReadLine();
        }

        #endregion

        #region actor sample
        static void ActorSample()
        {
            var system = ActorSystem.Create("AkkaDemo01");
            var gameId = Guid.NewGuid();
            var gameActorRef = system.ActorOf(GameActor.Props(gameId), $"Game-{gameId}");
            Console.WriteLine(gameActorRef.Path);
            var startGameMessage = new StartGame();

            var putBone = new PutBone() { Left = 0, Right = 0 };


            gameActorRef.Tell(putBone);
            //var gameState = await gameActorRef.Ask<BoardState>(putBone);
            //gameActorRef.Tell(startGameMessage);


            Console.ReadLine();
        }


        #endregion

    }
}
