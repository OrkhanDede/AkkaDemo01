namespace OneZeroOneDomino.Actors.Manager.Messages
{
    public class PlayGame
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public string GameId { get; set; }
    }
}
