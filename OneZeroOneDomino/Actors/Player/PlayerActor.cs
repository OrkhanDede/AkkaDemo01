using Akka.Actor;

namespace OneZeroOneDomino.Actors.Player
{
    public class PlayerActor:ReceiveActor
    {
        public string Id { get; }
        public string Name { get; }
        private int _point = 0;

        public PlayerActor(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
