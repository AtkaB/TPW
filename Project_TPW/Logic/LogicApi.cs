using Data;

namespace Logic
{
    public abstract class LogicApi
    {
        public abstract void CreateBalls(int number);
        public abstract int GetNumberOfBalls();
        public abstract double GetX(int number);
        public abstract double GetY(int number);
        public abstract event EventHandler LogicApiEvent;
        public static LogicApi Instance()
        {
            return new Logic();
        }
        private class Logic : LogicApi
        {
            DataApi dataApi;
            public Logic()
            {
                dataApi = DataApi.Instance();
                dataApi.BallEvent += Ball_PositionChanged;
            }
            public override event EventHandler LogicApiEvent;

            public override void CreateBalls(int number)
            {
                dataApi.CreateBalls(number);
            }
            public override int GetNumberOfBalls()
            {
                return dataApi.GetNumberOfBalls();
            }
            public override double GetX(int number)
            {
                return dataApi.GetX(number);
            }
            public override double GetY(int number)
            {
                return dataApi.GetY(number);
            }

            private void Ball_PositionChanged(object sender, EventArgs e)
            {
                CheckColisionWithWalls();
                LogicApiEvent?.Invoke(this, EventArgs.Empty);
            }

            private void CheckColisionWithWalls()
            {
                for (int i = 0; i < dataApi.GetNumberOfBalls(); i++)
                {
                    if (dataApi.GetX(i) < 0)
                    {
                        dataApi.SetHorizontalMove(i);
                    }
                    if (dataApi.GetY(i) < 0)
                    {
                        dataApi.SetVerticalMove(i);
                    }

                    if (dataApi.GetX(i) + 50 > 500)
                    {
                        dataApi.SetHorizontalMove(i);
                    }
                    if (dataApi.GetY(i) + 50 > 500)
                    {
                        dataApi.SetVerticalMove(i);
                    }
                }
            }
        }
    }
}
