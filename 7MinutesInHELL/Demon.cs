using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using _7MinutesInHELL.Properties;
namespace _7MinutesInHELL
{
    public class Demon
    {
        Point center;
        public static int width = 100;
        public static int height = 100;
        public static int velocity = 2;
        public Direction direction;
        Bitmap demon;
        public Demon(Point center, Direction direction, Bitmap demon)
        {
            this.center = center;
            this.direction = direction;
            this.demon = demon;
            demon.MakeTransparent();
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(demon, center.X - width/2, center.Y - height/2, width, height);
        }
        public bool Move(int right, int bottom)
        {
            
            if(direction == Direction.Up)
            {
                int newY = center.Y - velocity;
                if (newY - height / 2 < 40)
                    return false;
                center = new Point(center.X, newY);
            }
            if (direction == Direction.Down)
            {
                int newY = center.Y + velocity;
                if (newY + height / 2 > bottom)
                    return false;
                center = new Point(center.X, newY);
            }
            if (direction == Direction.Left)
            {
                int newX = center.X - velocity;
                if (newX - width / 2 < 0)
                    return false;
                center = new Point(newX, center.Y);
            }
            if (direction == Direction.Right)
            {
                int newX = center.X + velocity;
                if (newX + width / 2 > right)
                    return false;
                center = new Point(newX, center.Y);
            }
            return true;
        }
        public bool CollidesPlayer(Player player)
        {
            int newX = this.center.X - width / 2 + 5;
            int newY = this.center.Y - height / 2 + 5;
            int newWidth = width - 40;
            int newHeight = height - 10;
            if (Math.Sqrt((newX + newHeight / 2 - player.center.X) * (newX + newHeight / 2 - player.center.X) + (newY + newHeight / 2 - player.center.Y) * (newY + newHeight / 2 - player.center.Y)) <=
                newHeight / 2 + (player.height - 10) / 2)
                return true;
            return false;
        }
        public bool isHit(Projectile p)
        {
            int newX = this.center.X - width / 2 + 20;
            int newY = this.center.Y - height / 2 + 5;
            int newWidth = width - 40;
            int newHeight = height - 5;
            if (Math.Sqrt((newX + newHeight / 2 - p.center.X) * (newX + newHeight / 2 - p.center.X) + (newY + newHeight / 2 - p.center.Y) * (newY + newHeight / 2 - p.center.Y)) <=
                newHeight / 2 + Projectile.height / 2)
                return true;
            return false;
        }
    }
}
