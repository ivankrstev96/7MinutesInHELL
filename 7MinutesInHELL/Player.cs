using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using _7MinutesInHELL.Properties;

namespace _7MinutesInHELL
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    [Serializable]
    public class Player
    {
        public Point center;
        public static int velocity = 4;
        public int width = 100;
        public int height = 100;
        public Direction direction;
        public bool flagMoving;
        public Direction lastHorizontal;
        public Bitmap[] movingLeft;
        public Bitmap[] movingRight;
        public int counter;
        public int counter2;
        public bool flagProjectile;
        public bool alive;
        public Player(Point center)
        {
            this.center = center;
            direction = Direction.Right;
            flagMoving = false;
            lastHorizontal = Direction.Right;
            this.movingLeft = new Bitmap[4];
            movingLeft[0] = new Bitmap(Resources.playermove1);
            movingLeft[1] = new Bitmap(Resources.playermove2);
            movingLeft[2] = new Bitmap(Resources.playermove3);
            movingLeft[3] = new Bitmap(Resources.playermove4);
            this.movingRight = new Bitmap[4];
            movingRight[0] = new Bitmap(Resources.playermove1);
            movingRight[1] = new Bitmap(Resources.playermove2);
            movingRight[2] = new Bitmap(Resources.playermove3);
            movingRight[3] = new Bitmap(Resources.playermove4);
            flagProjectile = true;
            for(int i=0; i<4; i++)
            {
                movingLeft[i].MakeTransparent();
                movingRight[i].MakeTransparent();
                movingRight[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            counter = 0;
            counter2 = 0;
            alive = true;
        }
        public void Draw(Graphics g)
        {
            if (flagMoving)
            {
                if (lastHorizontal == Direction.Left)
                    g.DrawImage(movingLeft[counter], center.X - width / 2, center.Y - height / 2, width, height);
                else
                    g.DrawImage(movingRight[counter], center.X - width / 2, center.Y - height / 2, width, height);
                counter2 ++;
                if (counter2 >= 5)
                {
                    counter2 = 0;
                    counter++;
                    if (counter >= 4)
                        counter = 0;
                }
            }
            else
            {
                if (lastHorizontal == Direction.Left)
                    g.DrawImage(movingLeft[1], center.X - width / 2, center.Y - height / 2, width, height);
                else
                    g.DrawImage(movingRight[1], center.X - width / 2, center.Y - height / 2, width, height);
            }
            
        }
        public void Turn(Direction direction)
        {
            this.direction = direction;
        }
        public void notMoving()
        {
            flagMoving = false;
            counter = 0;
        }
        public void Move(int width, int height, int top)
        {
            if(direction == Direction.Up)
            {
                if (center.Y - velocity >= top)
                    center = new Point(center.X, center.Y - velocity);
                else
                    center = new Point(center.X, height - (velocity-center.Y + top));
            }
            if (direction == Direction.Down)
            {
                if (center.Y + velocity <= height)
                    center = new Point(center.X, center.Y + velocity);
                else
                    center = new Point(center.X, top + (center.Y + velocity) - height);
            }
            if (direction == Direction.Left)
            {
                if (center.X - velocity >= 0)
                    center = new Point(center.X - velocity, center.Y);
                else
                    center = new Point(width - (velocity-center.X),center.Y);
                lastHorizontal = Direction.Left;
            }
            if (direction == Direction.Right)
            {
                if (center.X + velocity <= width)
                    center = new Point(center.X + velocity, center.Y);
                else
                    center = new Point(0 + (center.X + velocity) - width, center.Y);
                lastHorizontal = Direction.Right;
            }
            flagMoving = true;
        }
    }
}
