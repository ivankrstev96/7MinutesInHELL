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
    public class Rock
    {
        public Point center;
        public static int width = 100;
        public static int height = 100;
        public Bitmap image;
        public Rock(Point center)
        {
            this.center = center;
            image = new Bitmap(Resources.rocks);
            image.MakeTransparent();
        }
        public void Draw(Graphics g)
        {
            g.DrawImage(image, center.X - width / 2, center.Y - height / 2, width, height);
            //Brush b = new SolidBrush(Color.Green);
            //g.FillEllipse(b, center.X - width / 2 + 20, center.Y - height / 2, width - 40, height - 50);

        }
        public bool Touches(Point center, int width, int height)
        {
            if(Math.Sqrt((this.center.X - center.X)*(this.center.X - center.X) + (this.center.Y - center.Y)*(this.center.Y - center.Y)) <= 
                Math.Sqrt(Rock.width*Rock.width + Rock.height*Rock.height)/2 + Math.Sqrt(width*width + height*height)/2 + 50)
            {
                return true;
            }
            return false;
        }
        public bool Colides(Point center, int width, int height)
        {
            int newX = this.center.X - Rock.width / 2 + 20;
            int newY = this.center.Y - Rock.height / 2;
            int newWidth = Rock.width - 40;
            int newHeight = Rock.height - 50;
            if (Math.Sqrt((newX+newHeight/2 - center.X) * (newX + newHeight / 2 - center.X) + (newY + newHeight / 2 - center.Y) * (newY + newHeight / 2 - center.Y)) <= 
                newHeight/2 + height/2)
            {
                return true;
            }
            return false;
        }
    }
}
