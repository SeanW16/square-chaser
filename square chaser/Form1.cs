using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

//Sean Woods, Tuesday May 7th 2024, basic square chaser game
namespace square_chaser
{
    public partial class Form1 : Form
    {
        //All shapes
        Rectangle top = new Rectangle(0, 20, 600, 10);
        Rectangle bottom = new Rectangle(0, 551, 600, 10);
        Rectangle left = new Rectangle(0, 0, 10, 600);
        Rectangle right = new Rectangle(574, 0, 10, 600);
        Rectangle player1 = new Rectangle(20, 40, 20, 20);
        Rectangle player2 = new Rectangle(544, 40, 20, 20);
        Rectangle ball = new Rectangle(300, 310, 10, 10);
        Rectangle powerUp = new Rectangle(300, 380, 10, 10); 

        int player1Score = 0;
        int player2Score = 0;
        int playerSpeed1 = 5;
        int playerSpeed2 = 5;

        bool wPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool aPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);

        Random randGen = new Random();

        //Sound players
        SoundPlayer player = new SoundPlayer(Properties.Resources.airPlaneDing);
        SoundPlayer player3 = new SoundPlayer(Properties.Resources.win);
        SoundPlayer player4 = new SoundPlayer(Properties.Resources.hitWall);

        public Form1()
        {
            InitializeComponent();
            //Randomized ball generation
            ball.X = randGen.Next(30, 532);
            ball.Y = randGen.Next(10, 554);
            powerUp.X = randGen.Next(30, 532);
            powerUp.Y = randGen.Next(10, 554);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Fill colors
            e.Graphics.FillRectangle(redBrush, top);
            e.Graphics.FillRectangle(redBrush, bottom);
            e.Graphics.FillRectangle(redBrush, left);
            e.Graphics.FillRectangle(redBrush, right);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.FillRectangle(greenBrush, player1);
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(yellowBrush, powerUp);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Key controls
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //Key controls 
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            PlayerMovement();

            //check if player1 hit the square
            if (player1.IntersectsWith(ball))
            {
                player.Play();
                int randY = randGen.Next(30, 532);
                int randX = randGen.Next(10, 554);
                ball.X = randX;
                ball.Y = randY;
                player1Score++;
                scoreLabel1.Text = $"Player 1 Score: {player1Score}";    
            }

            //check if player2 hit the square
            if (player2.IntersectsWith(ball))
            {
                player.Play();
                int randY = randGen.Next(30, 532);
                int randX = randGen.Next(10, 554);
                ball.X = randX;
                ball.Y = randY;
                player2Score++;
                scoreLabel2.Text = $"Player 2 Score: {player2Score}";           
            }

            //check if player1 hit the power up
            if (player1.IntersectsWith(powerUp))
            {
                player.Play();
                int randY = randGen.Next(30, 532);
                int randX = randGen.Next(10, 554);
                powerUp.X = randX;
                powerUp.Y = randY;
                playerSpeed1 = 10;   
                powerUpTimer.Start();
            }

            //check if player2 hit the power up
            if (player2.IntersectsWith(powerUp))
            {
                player.Play();
                int randY = randGen.Next(30, 532);
                int randX = randGen.Next(10, 554);
                powerUp.X = randX;
                powerUp.Y = randY;
                playerSpeed2 = 10;
                powerUpTimer2.Start();
            }

            //check to see who wins
            if (player1Score == 5)
            {
                player3.Play();
                winLabel.Text = "Player 1 Wins";
                gameTimer.Stop();
            }
            if (player2Score == 5)
            {
                player3.Play();
                winLabel.Text = "Player 2 Wins";
                gameTimer.Stop();
            }
            Refresh();
        }

        private void powerUpTimer_Tick(object sender, EventArgs e)
        {
            //resetting player1 speed after power up
            playerSpeed1 = 5;
        }

        private void powerUpTimer2_Tick(object sender, EventArgs e)
        {
            //resetting player2 speed after power up
            playerSpeed2 = 5;
        }

        public void PlayerMovement()
        {
            //move player 1
            if (wPressed == true)
            {
                player1.Y = player1.Y - playerSpeed1;

                if (player1.IntersectsWith(top))
                {
                    player4.Play();
                    player1.Y = 30;
                }
            }
            if (sPressed == true)
            {
                player1.Y = player1.Y + playerSpeed1;

                if (player1.IntersectsWith(bottom))
                {
                    player4.Play();
                    player1.Y = 531;
                }
            }
            if (aPressed == true)
            {
                player1.X = player1.X - playerSpeed1;

                if (player1.IntersectsWith(left))
                {
                    player4.Play();
                    player1.X = 10;
                }
            }
            if (dPressed == true)
            {
                player1.X = player1.X + playerSpeed1;

                if (player1.IntersectsWith(right))
                {
                    player4.Play();
                    player1.X = 554;
                }
            }

            //move player 2
            if (upPressed == true)
            {
                player2.Y = player2.Y - playerSpeed2;

                if (player2.IntersectsWith(top))
                {
                    player4.Play();
                    player2.Y = 30;
                }
            }
            if (downPressed == true)
            {
                player2.Y = player2.Y + playerSpeed2;

                if (player2.IntersectsWith(bottom))
                {
                    player4.Play();
                    player2.Y = 531;        
                }
            }
            if (rightPressed == true)
            {
                player2.X = player2.X + playerSpeed2;

                if (player2.IntersectsWith(right))
                {
                    player4.Play();
                    player2.X = 554;
                }
            }
            if (leftPressed == true)
            {
                player2.X = player2.X - playerSpeed2;

                if (player2.IntersectsWith(left))
                {
                    player4.Play();
                    player2.X = 10;
                }
            }
        }
    }
}
