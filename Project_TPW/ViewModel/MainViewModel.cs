using Model;
using System.ComponentModel;

namespace ViewModel
{
    public class MainViewModel
    {
        ModelApi Api { get; set; }
        public List<BallModel> Balls { get; set; }

        public MainViewModel() 
        {
            Api = ModelApi.Instance();
            Balls = Api.balls;
            Api.AddBalls(1);
        }
    }
}