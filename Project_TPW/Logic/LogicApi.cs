using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicApi
    {
        public Board Board { get; set; }
        public abstract void AddBall(Ball ball);
        public abstract void updatePosition();

        public static LogicApi Instance()
        {
            return new Logic();
        }
        private class Logic : LogicApi
        {
            public Logic() 
            {
                Board = new Board(500, 500);
            }
            public override void AddBall(Ball ball)
            {
                Board.AddBall(ball);
            }
            public override void updatePosition()
            {
                Board.updatePosition();
            }
        }
    }
}
