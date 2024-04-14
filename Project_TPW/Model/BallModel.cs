using Logic;
using System.ComponentModel;
using System.Xml.Linq;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        private readonly Ball Ball;
        public String Color { get; set; }
        private float x;
        public float X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Update()
        {
            X = Ball.X;
            Y = Ball.Y;
            System.Diagnostics.Debug.WriteLine("X: {0}, Y: {1}", X, Y);
        }
    }
}