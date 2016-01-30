using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameJam2016
{
    [Flags]
    public enum PlayerAction
    {
        None = 0,
        MoveLeft = 1,
        MoveRight = 2,
        Jump = 4,
        Shoot = 8,
        Fire = 16,
        Earth = 32,
        Water = 64,
        Air = 128,
    }
}
