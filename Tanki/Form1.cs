using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

namespace Tanki
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics gbmp;
        Wall w = new Wall ();
        Tank tank1, tank2;
        Bullet[] bullet1 = new Bullet[15];
        Bullet[] bullet2 = new Bullet[15];
        public Form1()
        {
            bmp = new Bitmap (@"C:\Users\Elibay\Desktop\Tanki\welcome.jpg");
            InitializeComponent();
            bmp = new Bitmap(bmp, new Size(pictureBox1.Width, pictureBox1.Height));
            pictureBox1.Image = bmp;
            gbmp = Graphics.FromImage(bmp);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            tank1 = new Tank(new Point(0, 600)); // first tank will in cor 0, 600
            tank2 = new Tank(new Point(950, 0)); // second tnak will in cor 950, 0
            bullet1 = new Bullet[15]; // creating bullets 1st tank 
            bullet2 = new Bullet[15]; // creating bullets 2nd tank
            for (int i = 1; i <= 10; ++i)
            {
                bullet1[i] = new Bullet(new Point(0, 0), 0);
                bullet2[i] = new Bullet(new Point(0, 0), 0); 
                bullet2[i].b = 1;
                bullet1[i].b = 1;
            }
            timer1.Enabled = true;
            tank1.Draw(gbmp); // drawing
            tank2.Draw(gbmp);
        }
        void Move ()
        {
            // main move function
            // move tanks
            tank1.Move(w);
            tank2.Move(w);
            // move bullets
            for (int i = 1; i <= 10; ++i)
                if (bullet1[i].b == 0)
                    bullet1[i].Move(w);
            for (int i = 1; i <= 10; ++i)
                if (bullet2[i].b == 0)
                    bullet2[i].Move(w);
        }
        void Check ()
        {
            // cheking tanks on bullet or not
            // 1st tank on bullet or not
            for (int i = 1; i <= 10; ++i)
            {
                if (bullet1[i].b == 0 && !bullet1[i].Check(tank2.pos))
                {
                    // first tank is won
                    timer1.Enabled = false;
                    gbmp.Clear(Color.Black);
                    Image img = Image.FromFile(@"C: \Users\Elibay\Desktop\Tanki\win1.jpg");
                    img = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));
                    gbmp.DrawImage(img, 0, 0);
                }
            }
            // 2nd tnak on bullet or not
            for (int i = 1; i <= 10; ++i)
            {
                if (bullet2[i].b == 0 && !bullet2[i].Check(tank1.pos))
                {
                    // second tank is won
                    timer1.Enabled = false;
                    gbmp.Clear(Color.Black);
                    Image img = Image.FromFile(@"C: \Users\Elibay\Desktop\Tanki\win2.jpg");
                    img = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));
                    gbmp.DrawImage(img, 0, 0);
                }
            }
        }
        void Draw ()
        {
            // drawing 
            w.Draw(gbmp);
            tank1.Draw(gbmp);
            tank2.Draw(gbmp);
            for (int i = 1; i <= 10; ++i)
                if (bullet1[i].b == 0)
                    bullet1[i].Draw(gbmp);
            for (int i = 1; i <= 10; ++i)
                if (bullet2[i].b == 0)
                    bullet2[i].Draw(gbmp);
        }
        void PrintCor()
        {
            // saving coridinates of tank and bullets in input.txt
            //File.Delete(@"C:\Users\Elibay\Desktop\Tanki\input.txt");
            StreamWriter rd = new StreamWriter(@"C:\Users\Elibay\Desktop\Tanki\input.txt");
            rd.WriteLine("Tank1 " + tank1.pos.X.ToString() + " " + tank1.pos.Y.ToString() + " " + tank1.d.ToString());
            rd.WriteLine("Tank2 " + tank2.pos.X.ToString() + " " + tank2.pos.Y.ToString() + " " + tank2.d.ToString());
            for (int i = 1; i <= 10; ++ i)
            {
                if (bullet1[i].b == 0)
                    rd.WriteLine("Bullet1 " + bullet1[i].pos.X.ToString() + " " + bullet1[i].pos.Y.ToString() + " " + bullet1[i].d);
            }
            for (int i = 1; i <= 10; ++i)
            {
                if (bullet2[i].b == 0)
                    rd.WriteLine("Bullet2 " + bullet2[i].pos.X.ToString() + " " + bullet2[i].pos.Y.ToString() + " " + bullet2[i].d);
            }
            rd.Close();
        }
        void Tank1_bot ()
        {
            // start bot1
            Process.Start(@"C:\Users\Elibay\Desktop\Tanki\bot1.exe");
            StreamReader rd = new StreamReader(@"C:\Users\Elibay\Desktop\Tanki\output1.txt");
            string s = rd.ReadLine();
            if (s == "Up")
            {
                tank1.d = 0;
            }
            if (s == "Right")
            {
                tank1.d = 1;
            }
            if (s == "Down")
            {
                tank1.d = 2;
            }
            if (s == "Left")
            {
                tank1.d = 3;
            }
            s = rd.ReadLine();
            if (s == "Push")
            {
                for (int i = 1; i <= 10; ++i)
                    if (bullet1[i].b == 1)
                    {
                        bullet1[i] = new Bullet(tank1.pos, tank1.d);
                        break;
                    }
            }
        }
        void Tank2_bot ()
        {
            //start bot2
            Process.Start(@"C:\Users\Elibay\Desktop\Tanki\bot1.exe");
            StreamReader rd = new StreamReader(@"C:\Users\Elibay\Desktop\Tanki\output2.txt");
            string s = rd.ReadLine();
            if (s == "Up")
            {
                tank2.d = 0;
            }
            if (s == "Right")
            {
                tank2.d = 1;
            }
            if (s == "Down")
            {
                tank2.d = 2;
            }
            if (s == "Left")
            {
                tank2.d = 3;
            }
            s = rd.ReadLine();
            if (s == "Push")
            {
                for (int i = 1; i <= 10; ++i)
                    if (bullet2[i].b == 1)
                    {
                        bullet2[i] = new Bullet(tank2.pos, tank2.d);
                        break;
                    }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            gbmp.Clear(Color.Black);
            Tank1_bot();
            Tank2_bot();
            Move();
            Draw();
            Check();
            PrintCor();
            pictureBox1.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // kerek emes zhai function :D
            if (e.KeyCode == Keys.W)
            {
                tank1.d = 0;
            }
            if (e.KeyCode == Keys.D)
            {
                tank1.d = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                tank1.d = 2;
            }
            if (e.KeyCode == Keys.A)
            {
                tank1.d = 3;
            }
            if (e.KeyCode == Keys.Space)
            {
                for (int i = 1; i <= 10; ++i)
                    if (bullet1[i].b == 1)
                    {
                        bullet1[i] = new Bullet(tank1.pos, tank1.d);
                        break;
                    }
            }
            if (e.KeyCode == Keys.Up)
            {
                tank2.d = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                tank2.d = 1;
            }
            if (e.KeyCode == Keys.Down)
            {
                tank2.d = 2;
            }
            if (e.KeyCode == Keys.Left)
            {
                tank2.d = 3;
            }
            if (e.KeyCode == Keys.P)
            {
                for (int i = 1; i <= 10; ++i)
                    if (bullet2[i].b == 1)
                    {
                        bullet2[i] = new Bullet(tank2.pos, tank2.d);
                        break;
                    }
            }
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }
    }
}
