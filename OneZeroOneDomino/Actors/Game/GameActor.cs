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
        private bool _gameHasStarted=false;
        private int _order { get; }
        public GameActor(string id)
        {
            _id = id;
            _players = new Dictionary<string, IActorRef>();
            _order = 0;
            Receive<StartGame>(StartGame);
            Receive<PlayerLeaveToGame>(PlayerLeave);
            Receive<ShuffledBone>(ShuffledBone);
        }

        private void ShuffledBone(ShuffledBone message)
        {
            
            //butun userlere suffle ile elaqeli event gonder
            throw new NotImplementedException();
        }

        private void ShuffleBone()
        {

        }

        private void StartGame(StartGame message)
        {
            if (message.Players.Count != 4)
            {
                return;
            }
            var players = message.Players;
            foreach (var playerInfoData in players)
            {
                var playerActorRef =
                    Context.ActorOf(Props.Create(() => new PlayerActor(playerInfoData.Id, playerInfoData.UserName)));
                _players[playerInfoData.Id] = playerActorRef;
            }

            _gameHasStarted = true;
        }
        private void PlayerLeave(PlayerLeaveToGame message)
        {
            if (_players.ContainsKey(message.PlayerId))
            {
                var playerActor = _players[message.PlayerId];
                playerActor.GracefulStop(TimeSpan.FromSeconds(1));
            }
        }
    }
}
