using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

//Joey Gerber 
//Mr. T
//ICS3U
// A classic game of air hockey, optimized so that the puck always is sent towards your opponents net. Can be a place for some very intense air hockey games.

namespace Air_Hockey
{
    public partial class Form1 : Form
    {
        //Location Variables (Players)
        int stick1X = 20;
        int stick1Y = 230;

        int stick2X = 780;
        int stick2Y = 230;
        int stickLength = 30;
        int stickWidth = 30;

        //Location Variables (Puck)
        int puckX = 410;
        int puckY = 230;
        int puckLength = 20;
        int puckWidth = 20;

        //goal variables
        int goal1X = 10;
        int goal1Y = 180;
        int goal2X = 786;
        int goal2Y = 180;
        int goal1Width = 4;
        int goal1Length = 100;

        int goal2Width = 4;
        int goal2Length = 100;

        //speed variables
        int puckXSpeed = 6;
        int puckYSpeed = 6;
        int stickSpeed = 13;

        //Key Variables
        bool wDown = false;
        bool sDown = false;
        bool upArrowUp = false;
        bool downArrowDown = false;
        bool dRight = false;
        bool aLeft = false;
        bool rightRight = false;
        bool leftLeft = false;

        //Score Variables

        int p2ScoreCount = 0;
        int p1ScoreCount = 0;

        //paint variables
        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Pen greenPen = new Pen(Color.Green);

        //sounds
        SoundPlayer impact = new SoundPlayer(Properties.Resources.clack);
        SoundPlayer beep = new SoundPlayer(Properties.Resources.beep);


        Pen borderPen = new Pen(Color.White, 7);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { //player 1 keys
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.D:
                    dRight = false;
                    break;
                case Keys.A:
                    aLeft = false;
                    break;
                    //player 2 keys
                case Keys.Up:
                    upArrowUp = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightRight = false;
                    break;
                case Keys.Left:
                    leftLeft = false;
                    break;
            }

        }



        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { //player 1 controls
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.D:
                    dRight = true;
                    break;
                case Keys.A:
                    aLeft = true;
                    break;
                //player 2 controls
                case Keys.Up:
                    upArrowUp = true;
                    break;
                case Keys.Right:
                    rightRight = true;
                    break;
                case Keys.Left:
                    leftLeft = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.FillRectangle(greenBrush, 408, 0, 4, 513 );
            g.FillEllipse(blueBrush, stick1X, stick1Y, 30, 30);
            g.FillEllipse(redBrush, stick2X, stick2Y, 30, 30);
            g.FillRectangle(whiteBrush, puckX, puckY, 20, 20);
            g.FillRectangle(whiteBrush, goal1X, goal1Y, goal1Width, goal1Length);
            g.FillRectangle(whiteBrush, goal2X, goal2Y, goal2Width, goal2Length);


            //aesthetic lines
            g.DrawEllipse(greenPen, 153, 100, 102, 102);
            g.DrawEllipse(greenPen, 153, 260, 102, 102);
            g.DrawEllipse(greenPen, 590, 100, 102, 102);
            g.DrawEllipse(greenPen, 590, 260, 102, 102);

        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //move puck

            puckX += puckXSpeed;
            puckY += puckYSpeed;

            //move player 1
            if (wDown == true && stick1Y > 0)
            {
                stick1Y -= stickSpeed;
            }

            if (sDown == true && stick1Y < 513 - stickLength)
            {
                stick1Y += stickSpeed;
            }
            if (aLeft == true && stick1X > 0)
            {
                stick1X -= stickSpeed;
            }
            if (dRight == true && stick1X < 408 - stickWidth)
            {
                stick1X += stickSpeed;
            }

            //move player 2

            if (downArrowDown == true && stick2Y < 513 - stickLength)
            {
                stick2Y += stickSpeed;
            }
            if (upArrowUp == true && stick2Y > 0)
            {
                stick2Y -= stickSpeed;
            }
            if (rightRight == true && stick2X < 786)
            {
                stick2X += stickSpeed;
            }
            if (leftLeft == true && stick2X > 410)
            {
                stick2X -= stickSpeed;
            }
            //wall collision

            if (puckY > 480 || puckY < 0)
            {
                puckYSpeed *= -1;
                impact.Play();

            }

            if (puckX < 0 || puckX > 820)
            {
                puckXSpeed *= -1;
                impact.Play();
            }

            //player collision(s) w/  rectangles

            Rectangle p1Collision = new Rectangle(stick1X, stick1Y, stickWidth, stickLength);
            Rectangle p2Collision = new Rectangle(stick2X, stick2Y, stickWidth, stickLength);
            Rectangle ballCollision = new Rectangle(puckX, puckY, puckWidth, puckLength);
            Rectangle goal1Collision = new Rectangle(goal2X, goal2Y, goal2Width, goal2Length);
            Rectangle goal2Collision = new Rectangle(goal1X, goal1Y, goal1Width, goal1Length);


            //player collisions if statements
            if (p1Collision.IntersectsWith(ballCollision))
            {
                puckXSpeed *= -1;
                puckX = stick1X + stickLength + 1;
                impact.Play();
            }
            else if (p2Collision.IntersectsWith(ballCollision))
            {
                puckXSpeed *= -1;
                puckX = stick2X - stickLength - 1;
                impact.Play();
            }


            //goal1 collision w/ ball
            if (goal1Collision.IntersectsWith(ballCollision))
            {
                p1ScoreCount++;
                p1Score.Text = $"{p1ScoreCount}";
                puckX = 410;
                puckY = 230;
                beep.Play();
            }
            //goal2 collision w/ ball
            else if (goal2Collision.IntersectsWith(ballCollision))
            {
                p2ScoreCount++;
                p2Score.Text = $"{p2ScoreCount}";
                puckX = 410;
                puckY = 230;
                beep.Play();
            }

            if (p1ScoreCount == 3)
            {
                p1Score.Text = "Winner";
                beep.Play();
                puckXSpeed = 0;
                puckYSpeed = 0;
            }
            if (p2ScoreCount == 3)
            {
                p2Score.Text = "Winner";
                beep.Play();
                puckXSpeed = 0;
                puckYSpeed = 0;
            }

            Refresh();

        }

        private void P2Score_Click(object sender, EventArgs e)
        {

        }
    }
}
