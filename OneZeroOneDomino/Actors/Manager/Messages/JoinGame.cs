using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace OneZeroOneDomino.Actors.Manager.Messages
{
    public class JoinGame
    {
        public string GameId { get; private set; }
    }
}
