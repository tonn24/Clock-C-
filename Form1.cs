using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clock
{
    public partial class Clock : Form
    {
        Timer timer = new Timer();
        int width = 300, height = 300, secHand = 140, minHand = 110, hrHand = 80;

        int cx, cy;

        Bitmap bmp;
        Graphics graphics;

        public Clock()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //make bitmap
            bmp = new Bitmap(width + 1, height + 1);

            
            cx = width / 2;
            cy = height / 2;

            this.BackColor = Color.White;

            //Aeg
            timer.Interval = 1000;
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //Graafika
            graphics = Graphics.FromImage(bmp);

            //aeg
            int seconds = DateTime.Now.Second;
            int minutes = DateTime.Now.Minute;
            int hour = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            graphics.Clear(Color.White);

            //joonista ring
            graphics.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, width, height);

            //joonista joonis
            graphics.DrawString("12", new Font("Times New Roman", 14), Brushes.Black, new Point(140, 2));
            graphics.DrawString("3", new Font("Times New Roman", 14), Brushes.Black, new Point(286, 140));
            graphics.DrawString("6", new Font("Times New Roman", 14), Brushes.Black, new Point(142, 282));
            graphics.DrawString("9", new Font("Times New Roman", 14), Brushes.Black, new Point(0, 140));

            //sekundi seier
            handCoord = msCoord(seconds, secHand);
            graphics.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //minuti seier
            handCoord = msCoord(minutes, minHand);
            graphics.DrawLine(new Pen(Color.Black, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            handCoord = hrCoord(hour% 12, minutes, hrHand);
            graphics.DrawLine(new Pen(Color.Gray, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            pictureBox1.Image = bmp;

            //Näita aega
            this.Text = "Clock  - " + hour + ":" + minutes + ":" + seconds;

            graphics.Dispose();
        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6; //iga minut ja sekund teeb 6 kraadi 

            if(val>=0 && val<=180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen *-Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        private int[] hrCoord(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            //iga tund teeb 30 kraadi ja iga minut 0.5 kraadi
            int val = (int)((hval*30)+(mval*0.5));

            if (val >= 0 && val <= 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = cx - (int)(hlen *-Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
