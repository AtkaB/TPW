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

    internal class Ball : IBall
    {
        private int _moveTime;
        private float _weight;
        private Vector2 _speed;
        private Vector2 _position;

        public Ball(int x, int y, float weight)
        {
            _weight = weight;
            Random rnd = new Random();
            Speed = new Vector2(x, y)
            {
                X = (float)(rnd.NextDouble() * (0.75 - (-0.75)) + (-0.75)),
                Y = (float)(rnd.NextDouble() * (0.75 - (-0.75)) + (-0.75))
            };
            Position = new Vector2(x, y)
            {
                X = x,
                Y = y
            };
            MoveTime = 1000/60;
            RunTask();
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }
        
        public int MoveTime
        {
            get => _moveTime;
            private set
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

        public float Weight { get => _weight; }

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
    }
}
