using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Star
    {
        private Point coordinate;
        private int timer, bonus;
        private bool available;
        private Image star = Image.FromFile(Snake_Game.Form1.folder + "star.png");

        public Star(Snake sn, Food fo)
        {
            ChangeLocation(sn, fo);
            timer = 0;
            bonus = 0;
            available = false;
        }

        public bool Available()
        {
            return available;
        }

        public void Available(bool temp)
        {
            available = temp;
        }

        public int Bonus(int temp)
        {
            bonus += temp;
            return bonus;
        }

        public int Timer()
        {
            return timer;
        }

        public void Timer(int temp)
        {
            timer += temp;
        }

        public Point Location()
        {
            return coordinate;
        } 

        public void RemoveStar()
        {
            available = false;
            bonus = 3;
        }

        public void SetStar(Snake sn, Food fo)
        {
            ChangeLocation(sn, fo);
            available = true;
            timer = 40;
            bonus = 0;
        }

        public void ChangeLocation(Snake sn, Food fo)
        {
            coordinate.X = Snake_Game.Form1.random.Next(41) * 15;
            coordinate.Y = Snake_Game.Form1.random.Next(41) * 15;

            if (sn.IntersectWith(coordinate) || coordinate.Equals(fo.Location()))
                ChangeLocation(sn, fo);
        }

        public void DrawStar(Graphics paper)
        {
            if (available)
            paper.DrawImage(star, coordinate);
        }
    }
}
