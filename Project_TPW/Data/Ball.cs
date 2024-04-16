using System;

namespace Data
{
    internal interface IBall
    {
        double X { get; set; }
        double Y { get; set; }
        double HorizontalMove { get; set; }
        double VerticalMove { get; set;}
        const int Radius = 50;
        double Weight { get; }
        void Move();
    }

    internal class Ball : IBall
    {
        private double _x;
        private double _y;
        private double _horizontalMove;
        private double _verticalMove;
        private double _weight;

        public Ball(int x, int y, int weight)
        {
            _x = x;
            _y = y;
            _weight = weight;
            Random rnd = new Random();
            HorizontalMove = rnd.NextDouble() * 20 - 10;
            VerticalMove = rnd.NextDouble() * 20 - 10;
            Task.Run(async () =>
            {
                while (true)
                {
                    Move();
                    await Task.Delay(1000/60); //1/60 sekundy
                }
            });
        }

        internal event EventHandler PositionChanged;

        internal void OnPositionChanged()
        {
            PositionChanged?.Invoke(this, EventArgs.Empty);
        }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPositionChanged();
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPositionChanged();
            }
        }

        public double HorizontalMove
        {
            get => _horizontalMove;
            set
            {
                _horizontalMove = value;
            }
        }

        public double VerticalMove
        {
            get => _verticalMove;
            set
            {
                _verticalMove = value;
            }
        }

        public double Weight { get => _weight; }

        public void Move()
        {
            X += HorizontalMove;
            Y += VerticalMove;
        }
    }
}
