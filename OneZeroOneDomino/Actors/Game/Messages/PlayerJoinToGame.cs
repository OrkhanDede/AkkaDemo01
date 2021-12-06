namespace OneZeroOneDomino.Actors.Game.Messages
{
    public class PlayerJoinToGame
    {
        public PlayerJoinToGame(string playerId, string playerName)
        {
            PlayerId = playerId;
            PlayerName = playerName;
        }
        public string PlayerId { get; set; }
        public string PlayerName { get; set; }
    }
}
