using System;
using System.Collections.Generic;
using Akka.Actor;
using OneZeroOneDomino.Actors.Game;
using OneZeroOneDomino.Actors.Game.Messages;
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
            Receive<PlayGame>(PlayGame);
        }
        private void PlayGame(PlayGame message)
        {
            IActorRef gameActorRef = null;
            if (!_games.ContainsKey(message.GameId))
            {
                var gameId = Guid.NewGuid().ToString();
                var gameActorObject = new GameActor(gameId);
                gameActorRef = Context.ActorOf(Props.Create(() => gameActorObject));
                _games[gameId] = gameActorRef;
            }
            else
            {
                gameActorRef = _games[message.GameId];
            }
            gameActorRef.Tell(new PlayerJoinToGame(message.UserId,message.UserName));
        }
        private void CreateNewGame(CreateGame message)
        {
            var gameId = Guid.NewGuid().ToString();
            var gameActor = Context.ActorOf(Props.Create(() => new GameActor(gameId)));
            _games[gameId] = gameActor;
        }
    }
}
