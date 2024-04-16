namespace Data
{
    public abstract class DataApi
    {
        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract event EventHandler BallEvent;
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract void SetHorizontalMove(int number);
        public abstract void SetVerticalMove(int number);

        public static DataApi Instance()
        {
            return new Data();
        }

        private class Data : DataApi
        {
            private Mutex mutex = new Mutex();
            private List<IBall> Balls { get; }
            public override int Width { get; }
            public override int Height { get; }
            public override event EventHandler BallEvent;
            public Data()
            {
                Balls = new List<IBall>();
                Width = 500;
                Height = 500;
            }
            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    mutex.WaitOne();
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300), 30);
                    Balls.Add(ball);
                    mutex.ReleaseMutex();
                    ball.PositionChanged += Ball_PositionChanged;
                }
            }
            public override int GetNumberOfBalls()
            {
                return Balls.Count;
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                Ball ball = sender as Ball;
                if (ball != null)
                {
                    BallEvent?.Invoke(ball, EventArgs.Empty);
                }
            }
            public override double GetX(int number)
            {
                return Balls[number].X;
            }
            public override double GetY(int number)
            {
                return Balls[number].Y;
            }
            public override void SetHorizontalMove(int number)
            {
                
                Balls[number].HorizontalMove *= -1;
            }

            public override void SetVerticalMove(int number)
            {
                Balls[number].VerticalMove *= -1;
            }
        }
    }
}