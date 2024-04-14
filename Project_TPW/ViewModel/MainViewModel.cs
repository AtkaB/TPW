using Model;
using System.Timers;

namespace ViewModel
{
    public class MainViewModel
    {
        ModelApi Api { get; set; }
        public List<BallModel> Balls { get; set; }

        private static System.Timers.Timer aTimer;

        public MainViewModel()
        {
            Api = ModelApi.Instance();
            Balls = Api.balls;
            Api.AddBalls(3);

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000 / 60;
            aTimer.Elapsed += OnUpdate;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnUpdate(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
            Api.updatePosition();
        }
    }
}