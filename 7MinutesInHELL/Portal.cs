using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using _7MinutesInHELL.Properties;

namespace _7MinutesInHELL
{
    
    public class Portal
    {
        public Point center;
        public bool isOpen;
        public bool isVisible;
        public static int width = 100;
        public static int height = 100;
        public Portal(Point center)
        {
            this.center = center;
            isOpen = false;
            isVisible = false;
        }
        public void open()
        {
            isOpen = true;
        }
        public void show()
        {
            isVisible = true;
        }
        public void Draw(Graphics g)
        {
            if (isVisible)
            {
                if (isOpen) {
                    Bitmap bmp = Resources.portalopen;
                    bmp.MakeTransparent();
                    g.DrawImage(bmp, center.X - width/2, center.Y - width/2, width, height);
                }
                else
                {
                    Bitmap bmp = Resources.portalclosed;
                    bmp.MakeTransparent();
                    g.DrawImage(bmp, center.X - width/2, center.Y - width/2, width, height);
                }
            }
        }
        public bool CollidesPlayer(Player player)
        {
            int newX = this.center.X - width / 2 + 30;
            int newY = this.center.Y - height / 2 + 10;
            int newWidth = width - 40;
            int newHeight = height - 80;
            if (isOpen)
            {
                if (Math.Sqrt((newX + newHeight / 2 - player.center.X) * (newX + newHeight / 2 - player.center.X) + (newY + newHeight / 2 - player.center.Y) * (newY + newHeight / 2 - player.center.Y)) <=
                newHeight / 2 + (player.height - 80) / 2)
                    return true;
            }
            return false;
        }
    }
}
