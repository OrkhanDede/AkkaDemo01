using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Dispatch.SysMsg;
using OneZeroOneDomino.Actors.Game;
using OneZeroOneDomino.Actors.Manager.Messages;

namespace OneZeroOneDomino.Actors.Manager
{
    public class GameManagerActor:ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _games;

        public GameManagerActor()
        {
            _games = new Dictionary<string, IActorRef>();
            Receive<CreateGame>(CreateNewGame);
        }

        private void CreateNewGame(CreateGame message)
        {
            var gameId = Guid.NewGuid().ToString();
            var gameActor = Context.ActorOf(Props.Create(() => new GameActor(gameId)));
            _games[gameId] = gameActor;
        }

        private void PlayerJoinToGame()
        {

        }
    }
}
