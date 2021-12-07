using System;
using Akka.Actor;
using OneZeroOneDomino.Actors.Game.Messages;
using OneZeroOneDomino.Actors.Player.Messages;

namespace OneZeroOneDomino.Actors.Player
{
    public class PlayerActor:ReceiveActor
    {
        public string Id { get; }
        public string Name { get; }
        public PlayerActor(string id, string name)
        {
            Id = id;
            Name = name;
            Receive<ShuffleBone>(Shuffle);
        }

        private void Shuffle(ShuffleBone message)
        {
            var rnd = new Random();
            var value=rnd.Next(1, 7); //[1,6]
            Sender.Tell(new ShuffledBone());
        }
    }
}
