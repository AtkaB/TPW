using System.Numerics;

namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract Vector2 GetPosition(int number);
        public abstract IBall GetBall(int number);
        public abstract event EventHandler BallEvent;
        public abstract DataApi Instance();

    }
}