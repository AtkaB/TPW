using System.Collections.ObjectModel;
using Logic;
using System.Numerics;

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
                    Vector2 position = LogicApi.GetPosition(i);
                    BallModel model = new BallModel(position.X, position.Y);
                    Balls.Add(model);
                }
            }

            private void LogicApiEventHandler()
            {
                for (int i = 0; i < LogicApi.GetNumberOfBalls(); i++)
                {
                    if (LogicApi.GetNumberOfBalls() == Balls.Count)
                    {
                        Vector2 position = LogicApi.GetPosition(i);
                        Balls[i].X = position.X;
                        Balls[i].Y = position.Y;
                    }
                }
            }
        }
    }
}
