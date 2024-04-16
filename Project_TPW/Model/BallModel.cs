using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        public String Color { get; set; }
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged();
            }
        }
        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged();
            }
        }
        public double Radious
        {
            get { return 50; }
        }


        public BallModel(double x, double y, string color = "White")
        {
            Color = color;
            X = x;
            Y = y;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}