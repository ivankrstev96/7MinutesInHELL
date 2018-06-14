using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using _7MinutesInHELL.Properties;

namespace _7MinutesInHELL
{
    [Serializable]
    public class PowerUp
    {
        public Point center;
        public static int width = 50;
        public static int height = 50;
        public int timeAlive;
        public PowerUp(Point center)
        {
            this.center = center;
            timeAlive = 30;
        }
        public void Draw(Graphics g)
        {
            Bitmap bmp = new Bitmap(Properties.Resources.bolt);
            bmp.MakeTransparent();
            g.DrawImage(bmp, center.X - width / 2, center.Y - height / 2, width, height);
        }
        public bool CollidesPlayer(Player player)
        {
            int newX = this.center.X - width / 2 + 10;
            int newY = this.center.Y - height / 2 + 5;
            int newWidth = width - 40;
            int newHeight = height - 20;
            if (Math.Sqrt((newX + newHeight / 2 - player.center.X) * (newX + newHeight / 2 - player.center.X) + (newY + newHeight / 2 - player.center.Y) * (newY + newHeight / 2 - player.center.Y)) <=
            newHeight / 2 + (player.height - 80) / 2)
                return true;
            return false;
        }
        public bool reduceTime()
        {
            timeAlive--;
            if (timeAlive == 0)
                return true;
            return false;
        }
    }
}
