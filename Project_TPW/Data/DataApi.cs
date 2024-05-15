namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract IBall GetBall(int number);
        public abstract event EventHandler BallEvent;
        public abstract DataApi Instance();

    }
}