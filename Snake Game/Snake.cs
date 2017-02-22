using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game
{
    class Snake
    {
        private List<Point> coordinate;
        public bool up, down, left, right, change_direction, pause, gameover;
        public int score, step_px = 15;
        private Image body = Image.FromFile(Snake_Game.Form1.folder + "body.png");
        private Image head = Image.FromFile(Snake_Game.Form1.folder + "head.png");
        
        public Snake()
        {
            coordinate = new List<Point>();
            up = false;
            down = false;
            left = false;
            right = false;
            change_direction = false;
            pause = false;
            gameover = true;
            score = 0;

            coordinate.Add(new Point(300, 300));
            coordinate.Add(new Point(300, 315));
            coordinate.Add(new Point(300, 330));
        }

        public List<Point> Coordinate()
        {
            return coordinate;
        }

        public void Add(Point a)
        {
            coordinate.Add(a);
        }

        public void Clear()
        {
            coordinate.Clear();
        }

        public int Length()
        {
            return coordinate.Count();
        }

        public void Move(int x, int y)
        {
            for (int i = coordinate.Count() - 1; i > 0; i--)
                coordinate[i] = coordinate[i - 1];

            Point temp = coordinate[0];
            if (temp.X + x > 600)
                temp.X = 0;
            else
                if (temp.X + x < 0)
                    temp.X = 600;
                else
                    temp.X += x;
            if (temp.Y + y > 600)
                temp.Y = 0;
            else
                if (temp.Y + y < 0)
                    temp.Y = 600;
                else temp.Y += y;

            coordinate[0] = temp;
        }

        public bool IntersectWith(Point a)
        {
            bool temp = false;
            for (int i = 1; i < coordinate.Count(); i++)
                if (coordinate[i].Equals(a))
                    temp = true;
            return temp;
        }

        public bool Bump()
        {
            bool temp = false;
            for (int i = 1; i < coordinate.Count(); i++)
                if (coordinate[0] == coordinate[i])
                    temp = true;
            return temp;
        }

        public void ChangeSprites()
        {
            if (Snake_Game.Form1.folder == "sprites_green/")
                Snake_Game.Form1.folder = "sprites_red/";
            else
                Snake_Game.Form1.folder = "sprites_green/";

            body = Image.FromFile(Snake_Game.Form1.folder + "body.png");
            head = Image.FromFile(Snake_Game.Form1.folder + "head.png");
        }

        public bool EatStar(Star st) { return (st.Available() && st.Location().Equals(coordinate[0])); }

        public bool EatFood(Food fo) { return (fo.Location().Equals(coordinate[0])); }

        public void DrawSnake(Graphics paper)
        {
            paper.DrawImage(head, coordinate[0]);
            for (int i = 1; i < coordinate.Count(); i++)
                paper.DrawImage(body, coordinate[i]);
        }
    }
}
