using Logic;

namespace Model
{
    public class BallModel
    {
        private readonly Ball Ball;
        public String Color { get; set; }
        public float X 
        {
            get { return Ball.X; }
        }
        public float Y
        {
            get { return Ball.Y; }
        }
        public float Radious
        {
            get { return Ball.Radius; }
        }

        public BallModel(Ball ball, string color = "White")
        {
            Ball = ball;
            Color = color;
        }
    }
}