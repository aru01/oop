using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tanki
{
    class Wall
    {
        public Image wall = Image.FromFile(@"C: \Users\Elibay\Desktop\Tanki\wall.jpg");
        public int[] x = new int[1000], y = new int[1000]; // coridinates of wall
        public Wall ()
        {
        	//creating cordinates 
            wall = new Bitmap(wall, new Size(30, 30));
            int k = 0;
            for (int i = 3; i <= 30; ++ i)
            {
                ++k;
                y[k] = 60;
                x[k] = i * 30; 
            }
            for (int i = 4; i <= 30; i += 5)
            {
                for (int j = 150; j <= 300; j += 30)
                {
                    ++k;
                    x[k] = i * 30;
                    y[k] = j;
                }
            }
            for (int i = 3; i <= 10; ++ i)
            {
                ++k;
                y[k] = 390;
                x[k] = i * 30;
                ++k;
                y[k] = 480;
                x[k] = i * 30;
            }
            for (int i = 23; i <= 30; ++ i)
            {
                ++k;
                y[k] = 390;
                x[k] = i * 30;
                ++k;
                y[k] = 480;
                x[k] = i * 30;
            }
            for (int i = 3; i <= 30; ++i)
            {
                ++k;
                y[k] = 570;
                x[k] = i * 30;
            }

        }
        public void Draw (Graphics g)
        {
        	// draw function
            for (int i = 1; i <= 124; ++i)
                g.DrawImage(wall, x[i], y[i]);
        }

    }
}
