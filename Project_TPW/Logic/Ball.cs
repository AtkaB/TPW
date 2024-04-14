namespace Logic
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float HorizontalSpeed { get; set; }
        public float VerticalSpeed { get; set; }
        public int Radius { get; set; }

        public Random rnd;
        public int Speed = 4;

        public Ball(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
            HorizontalSpeed = 0;
            VerticalSpeed = 0;

            rnd = new Random();
        }

        public void updatePosition(Board board)
        {
            HorizontalSpeed = rnd.NextSingle() * Speed - Speed / 2;
            VerticalSpeed = rnd.NextSingle() * Speed - Speed / 2;

            X += HorizontalSpeed;
            Y += VerticalSpeed;

            if (X < 0) { X = 0; }
            if (Y < 0) { Y = 0; }

            if (X + Radius > board.Width) { X = board.Width - Radius; }
            if (Y + Radius > board.Height) { Y = board.Height - Radius; }
        }
    }
}