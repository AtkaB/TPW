using System;
using System.Numerics;

namespace Data
{
    public interface IBall
    {
        Vector2 Position { get; }
        int MoveTime { get; }
        const int Radius = 50;
        float Weight { get; }
        Vector2 Speed { get; set; }
    }

    


}
