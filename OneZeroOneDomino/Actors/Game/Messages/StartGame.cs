using System.Collections.Generic;
namespace OneZeroOneDomino.Actors.Game.Messages
{
    public class StartGame
    {
        public List<UserInfoData> Players { get; private set; }
        public StartGame(List<UserInfoData> players)
        {
            Players = players;
        }
    }

    public class UserInfoData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int Point { get; set; }
    }
}
