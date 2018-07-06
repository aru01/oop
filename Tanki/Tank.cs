using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Tanki
{
    class Tank
    {
        public Image pic = Image.FromFile(@"C: \Users\Elibay\Desktop\Tanki\tank.png");
        public Point pos; // postion of tank
        public int d = 0; // direction
        int[] dx = { 0, 2, 0, -2 }; // speed 2
        int[] dy = { -2, 0, 2, 0 };
        public Tank (Point X)
        {
            pic = new Bitmap(pic, new Size(40, 40)); // size 40x40
            pos = X;
        }
        bool Can (Wall wall)
        {
            // function that check can we move tan or not
            Point to = new Point(pos.X + dx[d], pos.Y + dy[d]);
            for (int i = 1; i <= 124; ++ i)
            {
                if (to.X > 950 || to.X < 0 || to.Y > 600 || to.Y < 0)
                    return false;
                if (to.X >= wall.x[i] - 35 && to.X <= wall.x[i] + 25)
                    if (to.Y >= wall.y[i] - 35 && to.Y <= wall.y[i] + 25)
                        return false;
            }
            return true;
        }
        public void Move (Wall wall)
        {
            // main moving function
            if (Can(wall))
            {
                pos.X += dx[d];
                pos.Y += dy[d];
            }
        }
        Image rotate90(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipY);
            return bmp;
        }
        Image rotate180(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            return bmp;
        }
        Image rotate95 (Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            return bmp;
        }
        public void Draw(Graphics g)
        {
            // drawing function
            if (d == 0)
            {
                g.DrawImage(pic, pos);
            }
            if (d == 1)
            {
                pic = rotate90(pic);
                g.DrawImage(pic, pos);
                pic = rotate90(pic);
            }
            if (d == 3)
            {
                pic = rotate95(pic);
                g.DrawImage(pic, pos);
                pic = rotate95(pic);
            }
            if (d == 2)
            {
                pic = rotate180(pic);
                g.DrawImage(pic, pos);
                pic = rotate180(pic);
            }

        }
    }
}
