using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Tanki
{
    class Bullet
    {
        public Image pic = Image.FromFile(@"C: \Users\Elibay\Desktop\Tanki\bullet.png"); // image of bullet
        public Point pos; // position
        public int d, b; // d is direct; b is bool that check we use this bullet or not
        int[] dx = { 0, 5, 0, -5 }; // speed is 5 in this directions we change the bullet 
        int[] dy = { -5, 0, 5, 0 };
        public Bullet(Point X, int x)
        {
            // creating new bullet size 20 20 
            pic = new Bitmap(pic, new Size(20, 20));
            pos.X = X.X + 10;
            pos.Y = X.Y + 10;
            b = 0;
            d = x;

        }
        bool Can(Wall wall)
        {
            // function that check we can move the bullet or not
            Point to = new Point(pos.X + dx[d], pos.Y + dy[d]);
            for (int i = 1; i <= 124; ++i)
            {
                if (to.X > 1500 || to.X < 0 || to.Y > 1000 || to.Y < 0)
                    return false;
                if (to.X >= wall.x[i] - 15 && to.X <= wall.x[i] + 25)
                    if (to.Y >= wall.y[i] - 15 && to.Y <= wall.y[i] + 25)
                        return false;
            }
            return true;
        }
        public void Move(Wall wall)
        {
            if (Can(wall))
            {
                // if we can we move 
                pos.X += dx[d];
                pos.Y += dy[d];
            }
            else
                b = 1; // or delete this bullet 
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
        Image rotate95(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
            return bmp;
        }
        public void Draw(Graphics g)
        {
            // drawing bullet 
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
        public bool Check (Point X)
        {
            // this funciton check bullet in tank or not
            Point y = pos;
            if (d == 1 || d == 3)
                y.Y += 5;
            else
                y.X += 5;
            Rectangle a;
            X.X += 3;
            if (d == 1 || d == 3)
                a = new Rectangle(y, new Size(20, 10));
            else
                a = new Rectangle(y, new Size(10, 20));
            Rectangle b = new Rectangle(X, new Size(35, 35));
            if (a.IntersectsWith (b))
                return false;
            return true;
        }
    }
}
