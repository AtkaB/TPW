﻿using Data;
using System.Numerics;

namespace Logic
{

    class Data : DataApi
    {
        private Logger _logger;
        private List<IBall> Balls { get; }
        public override int Width { get; }
        public override int Height { get; }
        public override event EventHandler BallEvent;

        public Data()
        {
            Balls = new List<IBall>();
            Width = 500;
            Height = 500;
            _logger = new Logger(Balls); // Pass the balls list to the logger
        }

        public override void CreateBalls(int number)
        {
            Random rnd = new Random();
            int a = Balls.Count;
            for (int i = 0; i < number; i++)
            {
                Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300), 10, i + a);
                Balls.Add(ball);
                ball.PositionChanged += Ball_PositionChanged;
            }
        }

        public override int GetNumberOfBalls()
        {
            return Balls.Count;
        }

        private void Ball_PositionChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                BallEvent?.Invoke(sender, EventArgs.Empty);
            }
        }

        public override Vector2 GetPosition(int number)
        {
            return Balls[number].Position;
        }

        public override IBall GetBall(int number)
        {
            return Balls[number];
        }

        public override DataApi Instance()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class LogicApi
    {
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract Vector2 GetPosition(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicApi Instance(DataApi dataApi)
        {
            if (dataApi == null)
            {
                return new Logic(new Data());
            }
            else
            {
                return new Logic(dataApi);
            }
        }
        private class Logic : LogicApi
        {
            DataApi dataApi;
            object _lock = new object();
            public Logic(DataApi api)
            {
                dataApi = api;
                dataApi.BallEvent += Ball_PositionChanged;
            }
            public override event EventHandler LogicApiEvent;

            public override void CreateBalls(int number)
            {
                dataApi.CreateBalls(number);
            }
            public override int GetNumberOfBalls()
            {
                return dataApi.GetNumberOfBalls();
            }
            public override Vector2 GetPosition(int number)
            {
                return dataApi.GetPosition(number);
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                IBall ball = (IBall)sender;
                if (ball != null)
                {
                    CheckCollisionWithBalls(ball);
                    CheckCollisionWithWalls(ball);
                    LogicApiEvent?.Invoke(this, EventArgs.Empty);
                }
            }

            private void CheckCollisionWithWalls(IBall ball)
            {
                Vector2 newSpeed = ball.Speed;
                if (ball.Position.X <= 0)
                {
                    newSpeed.X = Math.Abs(ball.Speed.X);
                }
                if (ball.Position.Y <= 0)
                {
                    newSpeed.Y = Math.Abs(ball.Speed.Y);
                }

                if (ball.Position.X + IBall.Radius >= dataApi.Width)
                {
                    newSpeed.X = -Math.Abs(ball.Speed.X);
                }
                if (ball.Position.Y + IBall.Radius > dataApi.Height)
                {
                    newSpeed.Y = -Math.Abs(ball.Speed.Y);
                }
                ball.Speed = newSpeed;
            }

            private void CheckCollisionWithBalls(IBall ball)
            {
                lock (_lock)
                {
                    for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
                    {
                        IBall secondBall = dataApi.GetBall(i);
                        if (secondBall != ball)
                        {
                            double d = Vector2.Distance(ball.Position, secondBall.Position);
                            if (d - (IBall.Radius) <= 0)
                            {
                                Vector2 firstSpeed = CountNewSpeed(ball, secondBall);
                                Vector2 secondSpeed = CountNewSpeed(secondBall, ball);
                                if (Vector2.Distance(ball.Position, secondBall.Position) >
                                    Vector2.Distance(ball.Position + firstSpeed * 1000 / 60, secondBall.Position + secondSpeed * 1000 / 60))
                                {
                                    return;
                                }
                                ball.Speed = firstSpeed;
                                secondBall.Speed = secondSpeed;
                            }
                        }
                    }
                }
            }

            private Vector2 CountNewSpeed(IBall ball, IBall secondBall)
            {
                return ball.Speed -
                       (2 * secondBall.Weight / (ball.Weight + secondBall.Weight)
                       * (Vector2.Dot(ball.Speed - secondBall.Speed, ball.Position - secondBall.Position)
                       * (ball.Position - secondBall.Position))
                       / (float)Math.Pow(Vector2.Distance(secondBall.Position, ball.Position), 2));
            }
        }
    }
}
