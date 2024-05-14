using Logic;
using Data;
using System.Numerics;

namespace LogicTest
{
    [TestClass]
    public class LogicApiUT
    {
        private class Ball : IBall
        {
            public static int Counter = 0;
            private int _moveTime;
            private float _weight;
            private Vector2 _speed;
            private Vector2 _position;
            public int MoveTime
            {
                get => _moveTime;
                set
                {
                    _moveTime = value;
                }
            }
            public Vector2 Speed
            {
                get { return _speed; }
                set
                {
                    _speed = value;
                }
            }
            public Vector2 Position
            {
                get => _position;
                private set { _position = value; }
            }
            internal event EventHandler PositionChanged;
            internal void OnPositionChanged()
            {
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
            public float Weight { get => _weight; }
            public Ball(int x, int y, float weight)
            {
                _weight = weight;
                Random rnd = new Random();
                if (Counter == 0)
                {
                    Speed = new Vector2(x, y)
                    {
                        X = 2,
                        Y = 0
                    };
                    Position = new Vector2(x, y)
                    {
                        X = 100,
                        Y = 100
                    };
                }
                else
                {
                    Speed = new Vector2(x, y)
                    {
                        X = 1,
                        Y = 0
                    };
                    Position = new Vector2(x, y)
                    {
                        X = 250,
                        Y = 100
                    };
                }
                Counter++;
                MoveTime = 1000 / 60;
                RunTask();
            }
            public void Move()
            {
                Position += Speed * MoveTime;
                OnPositionChanged();
            }
            private void RunTask()
            {
                Task.Run(async () =>
                {
                    while (true)
                    {
                        Move();
                        await Task.Delay(MoveTime);
                    }
                });
            }
            public void SetPosition(Vector2 position)
            {
                Position = position;
            }
        }
        private class Data : DataApi
        {
            public List<IBall> Balls { get; }
            public override int Width { get; }
            public override int Height { get; }
            public override event EventHandler BallEvent;
            public Data()
            {
                Balls = new List<IBall>();
                Width = 500;
                Height = 500;
                Ball.Counter = 0;
            }
            public override void CreateBalls(int number)
            {
                Random rnd = new Random();
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(rnd.Next(100, 300), rnd.Next(100, 300), rnd.Next(7, 12));
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
            public override double GetX(int number)
            {
                return Balls[number].Position.X;
            }
            public override double GetY(int number)
            {
                return Balls[number].Position.Y;
            }
            public override IBall GetBall(int number)
            {
                return Balls[number];
            }
        }

        LogicApi logicApi = LogicApi.Instance(new Data());
        [TestMethod]
        public void InstanceTest()
        {
            Assert.IsNotNull(logicApi);
        }

        [TestMethod]
        public void AddBallTest()
        {
            Assert.IsNotNull(logicApi);
            logicApi.CreateBalls(1);
            Assert.AreEqual(1, logicApi.GetNumberOfBalls());
            logicApi.CreateBalls(1);
            Assert.AreEqual(2, logicApi.GetNumberOfBalls());
        }

        [TestMethod]
        public void CheckCollisionWithWallTest()
        {
            LogicApi api = LogicApi.Instance(new Data());
            api.CreateBalls(1);
            Assert.AreEqual(1, api.GetNumberOfBalls());
            double prev_x = api.GetX(0);
            api.LogicApiEvent += (sender, args) =>
            {
                if (prev_x > api.GetX(0))
                {
                    Assert.IsTrue(api.GetX(0) < 450);
                    return;
                }
                prev_x = api.GetX(0);
            };
        }

        [TestMethod]
        public void CheckCollisionWithBall()
        {
            LogicApi api = LogicApi.Instance(new Data());
            api.CreateBalls(2);
            Assert.AreEqual(2, api.GetNumberOfBalls());
            double prev = CountDistance(api.GetX(0), api.GetX(1));
            int testFlag = 0;
            api.LogicApiEvent += (sender, args) =>
            {
                if (testFlag == 0 && prev <= CountDistance(api.GetX(0), api.GetX(1)))
                {
                    testFlag = 1;
                }
                if (testFlag == 1)
                {
                    Assert.IsTrue(prev > CountDistance(api.GetX(0), api.GetX(1)));
                    return;
                }
                prev = CountDistance(api.GetX(0), api.GetX(1));
            };
        }
        
        private double CountDistance(double x1, double x2)
        {
            return Math.Abs(x1 - x2);
        }
    }
}