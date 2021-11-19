using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaDemo01.Actors.Game.Model
{
    public class BoardState
    {
        public List<Bone> Bones { get; set; } = new List<Bone>();
        public bool PutLeft(Bone bone)
        {
            Bones.Add(bone);
            return true;
        }

        public bool PutRight(Bone bone)
        {
            Bones.Add(bone);
            return true;
        }
    }
}
