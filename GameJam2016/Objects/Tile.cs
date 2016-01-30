using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2016.Objects
{
    public class Tile
    {
        public bool Passable
        {
            get
            {
                return Type != '*';
            }
        }

        public char Type { get; set; }
    }
}
