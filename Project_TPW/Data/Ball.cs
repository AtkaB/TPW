using System;
using System.Numerics;

namespace Data
{
    public interface IBall
    {
        int ID { get; }
        Vector2 Position { get; }
        const int Radius = 50;
        float Weight { get; }
        Vector2 Speed { get; set; }
    }

    


}
