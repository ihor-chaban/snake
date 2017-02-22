using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Food
    {
        private Point coordinate;
        private Image apple = Image.FromFile(Snake_Game.Form1.folder + "apple.png");

        public Food(Snake sn)
        {
            coordinate.X = Snake_Game.Form1.random.Next(41) * 15;
            coordinate.Y = Snake_Game.Form1.random.Next(41) * 15;

            while (sn.IntersectWith(coordinate))
            {
                coordinate.X = Snake_Game.Form1.random.Next(41) * 15;
                coordinate.Y = Snake_Game.Form1.random.Next(41) * 15;
            }
        }

        public Point Location()
        {
            return coordinate;
        }

        public void ChangeLocation(Snake sn, Star fo)
        {
            coordinate.X = Snake_Game.Form1.random.Next(41) * 15;
            coordinate.Y = Snake_Game.Form1.random.Next(41) * 15;

            if (sn.IntersectWith(coordinate) || (fo.Available() && coordinate.Equals(fo.Location())))
                ChangeLocation(sn, fo);
        }

        public void DrawFood(Graphics paper)
        {
            paper.DrawImage(apple, coordinate);
        }
    }
}
