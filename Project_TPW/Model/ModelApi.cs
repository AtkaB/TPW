using System.Collections.ObjectModel;
using Logic;

namespace Model
{
    public abstract class ModelApi
    {
        public LogicApi LogicApi;
        public ObservableCollection<BallModel> Balls;
        public abstract void AddBalls(int number);
        public static ModelApi Instance()
        {
            return new Model();
        }

        private class Model : ModelApi
        {
            public Model() 
            {   
                Balls = new ObservableCollection<BallModel>();
                LogicApi = LogicApi.Instance(null);
                LogicApi.LogicApiEvent += (sender, args) => LogicApiEventHandler();
            }

            public override void AddBalls(int number)
            {
                LogicApi.CreateBalls(number);
                for (int i = 0; i < number; i++)
                {
                    BallModel model = new BallModel(LogicApi.GetX(i), LogicApi.GetY(i));
                    Balls.Add(model);
                }
            }

            private void LogicApiEventHandler()
            {
                for (int i = 0; i < LogicApi.GetNumberOfBalls(); i++)
                {
                    if (LogicApi.GetNumberOfBalls() == Balls.Count)
                    {
                        Balls[i].X = LogicApi.GetX(i);
                        Balls[i].Y = LogicApi.GetY(i);
                    }
                }
            }
        }
    }
}
