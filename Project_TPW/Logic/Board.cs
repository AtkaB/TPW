using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class Board
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Ball> Balls { get; set; }
        public Board(int width, int height) 
        {
            Width = width;
            Height = height;
            Balls = new List<Ball>();
        }

        public void AddBall(Ball b) 
        {
            Balls.Add(b);
        }
    }
}
