using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public abstract class ModelApi
    {
        public LogicApi logicApi;
        public List<BallModel> balls;
        public int BoardWidth { get; set; }
        public int BoardHeight { get; set; }
        public abstract void AddBalls(int number);
        public abstract void updatePosition();
        public static ModelApi Instance()
        {
            return new Model();
        }

        private class Model : ModelApi
        {
            public Model()
            {
                balls = new List<BallModel>();
                logicApi = LogicApi.Instance();
                BoardWidth = logicApi.Board.Width;
                BoardHeight = logicApi.Board.Height;
            }

            public override void AddBalls(int number)
            {
                for (int i = 0; i < number; i++)
                {
                    Ball ball = new Ball(490 + i * 10, 500, 10);
                    logicApi.AddBall(ball);
                    balls.Add(new BallModel(ball));
                }
            }

            public override void updatePosition()
            {
                logicApi.updatePosition();
                foreach (BallModel ball in balls)
                {
                    ball.Update();
                }
            }
        }
    }
}