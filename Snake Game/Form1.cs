using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        static public string folder = "sprites_green/";
        static public Random random = new Random();

        Graphics paper;
        static Snake snake = new Snake();
        static Food food = new Food(snake);
        static Star star = new Star(snake, food);
        
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(615, 637);
        }

        private void Form1_Load(object sender, EventArgs e) {   }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            paper = e.Graphics;
            star.DrawStar(paper);
            food.DrawFood(paper);
            snake.DrawSnake(paper);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Up || e.KeyData == Keys.W) && !snake.down && !snake.change_direction)
            {
                snake.change_direction = true;
                snake.up = true;
                snake.down = false;
                snake.left = false;
                snake.right = false;
            } else
                if ((e.KeyData == Keys.Down || e.KeyData == Keys.S) && !snake.up && !snake.change_direction)
                {
                    snake.change_direction = true;
                    snake.up = false;
                    snake.down = true;
                    snake.left = false;
                    snake.right = false;
                } else
                    if ((e.KeyData == Keys.Left || e.KeyData == Keys.A) && !snake.right && !snake.change_direction)
                    {
                        snake.change_direction = true;
                        snake.up = false;
                        snake.down = false;
                        snake.left = true;
                        snake.right = false;
                    } else
                        if ((e.KeyData == Keys.Right || e.KeyData == Keys.D) && !snake.left && !snake.change_direction)
                        {
                            snake.change_direction = true;
                            snake.up = false;
                            snake.down = false;
                            snake.left = false;
                            snake.right = true;
                        }

            if (e.KeyData == Keys.Escape)
                Application.Exit();

            if (e.KeyData == Keys.P)
            {
                snake.pause = !snake.pause;
                timer1.Enabled = !snake.pause;

                if (!timer1.Enabled)
                    spaceBarLabel.Text = "Press P to Continue";
                else
                    spaceBarLabel.Text = "";
            }

            if (e.KeyData == Keys.Space)
            {
                if (snake.gameover)
                {
                    spaceBarLabel.Text = "";
                    timer1.Enabled = true;
                    snake.gameover = false;
                    snake.up = true;
                    snake.down = false;
                    snake.left = false;
                    snake.right = false;
                    snake.change_direction = false;
                    snake.pause = false;
                }
            }

            if (e.KeyData == Keys.Q)
            {
                snake.ChangeSprites();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (snake.up) { snake.Move(0, -snake.step_px); }
            if (snake.down) { snake.Move(0, snake.step_px); }
            if (snake.left) { snake.Move(-snake.step_px, 0); }
            if (snake.right) { snake.Move(snake.step_px, 0); }

            if (snake.change_direction)
                snake.change_direction = false;

            snake.score += 10;
            snakeScoreLabel.Text = Convert.ToString(snake.score);

            if (star.Available())
            {
                starProgressBar.Value = star.Timer();
                star.Timer(-1);
                if (star.Timer() < 0)
                {
                    star.Available(false);
                    starProgressBar.Visible = false;
                }
            }

            if (snake.Bump())
                Restart();

            if (snake.EatFood(food))
            {
                snake.score += 1000;
                snake.Add(food.Location());
                food.ChangeLocation(snake, star);

                if (!star.Available() && Snake_Game.Form1.random.Next(10) == 0)
                {
                    star.SetStar(snake, food);
                    starProgressBar.Value = star.Timer();
                    starProgressBar.Visible = true;
                }
            }

            if (snake.EatStar(star))
            {
                snake.score += 10000;
                star.RemoveStar();
                starProgressBar.Visible = false;
            }

            if (star.Bonus(-1) > 0)
                snake.Add(star.Location());

            this.Invalidate();
        }

        public void Restart()
        {
            timer1.Enabled = false;
            snake.gameover = true;
            if (star.Available())
            {
                star.Available(false);
                starProgressBar.Value = 0;
                starProgressBar.Visible = false;
            }
            MessageBox.Show("You Lose!\r\nYour Score: " + Convert.ToString(snake.score) + "\r\n\r\nBest Scores\r\n" + Scoreboard.AddScore(snake), "Game Over!");
            snakeScoreLabel.Text = "0";
            snake.score = 0;
            spaceBarLabel.Text = "Controls\r\nWASD and Arrows\r\n\r\nPress Space to Begin\r\nPress Q to Change Snake\r\nPress P to Pause\r\nPress Esc to Exit";
            snake.Clear();
            snake.Add(new Point(300, 300));
            snake.Add(new Point(300, 315));
            snake.Add(new Point(300, 330));
        }
    }
}
