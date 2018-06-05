using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _7MinutesInHELL
{
    [Serializable]
    public class Projectile
    {
        public Point center;
        public static int velocity = 10;
        public static int height = 6;
        public static int width = 20;
        public Direction direction;
        public Projectile(Point center, Direction direction)
        {
            this.center = center;
            this.direction = direction;
        }
        public void Draw(Graphics g)
        {
            Brush b = new SolidBrush(Color.Aqua);
            g.FillEllipse(b, center.X - width / 2, center.Y - height / 2, width, height);
            b.Dispose();
        }
        public bool Move(int right, int bottom, int top)
        {
            if(direction == Direction.Left)
            {
                center = new Point(center.X - velocity, center.Y);
                if (center.X - width/2 <= 0 || center.Y - height/2 <= top || center.Y + height >= bottom)
                {
                    return false;
                }
                    

            }
            else
            {
                center = new Point(center.X + velocity, center.Y);
                if(center.X + width/2 >= right || center.Y - height / 2 <= top || center.Y + height >= bottom)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
