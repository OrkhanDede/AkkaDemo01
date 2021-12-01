using System;
using System.Collections.Generic;
using Akka.Actor;
using OneZeroOneDomino.Actors.Game.Messages;
using OneZeroOneDomino.Actors.Player;

namespace OneZeroOneDomino.Actors.Game
{
    public class GameActor: ReceiveActor
    {
        private readonly Dictionary<string, IActorRef> _players;
        private readonly string _id;

        public GameActor(string id)
        {
            _id = id;
            _players = new Dictionary<string, IActorRef>();
            Receive<PlayerJoinToGame>(PlayerJoin);
            Receive<PlayerLeaveToGame>(PlayerLeave);
        }
        private void PlayerJoin(PlayerJoinToGame message)
        {
            if(_players.Count==4) return;
            if (!_players.ContainsKey(message.PlayerId))
            {
                var playerId = message.PlayerId;
                var playerActor=
                    Context.ActorOf(Props.Create<PlayerActor>(() => new PlayerActor(playerId, $"Player-{playerId}")));
                _players[playerId] = playerActor;
            }
        }
        private void PlayerLeave(PlayerLeaveToGame message)
        {
            if (_players.ContainsKey(message.PlayerId))
            {
                var playerActor = _players[message.PlayerId];
                playerActor.GracefulStop(TimeSpan.FromSeconds(1));
            }
        }

        private void StartGame(StartGame message)
        { 
            //daslari qarisdir ve daslari oyuncular arasinda bolusdur.
        }
    }
}
