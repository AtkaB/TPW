namespace Logic
{
    public class Ball
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }

        public Ball(float x, float y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }
    }
}