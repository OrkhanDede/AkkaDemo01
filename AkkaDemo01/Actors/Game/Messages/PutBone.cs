using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaDemo01.Actors.Game.Messages
{
    public record PutBone
    {
        public ushort Left { get; init; }
        public ushort Right { get; init; }
    }
}
