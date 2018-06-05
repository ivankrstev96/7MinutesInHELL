using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _7MinutesInHELL
{
    [Serializable]
    public class GameDoc
    {
        public Player player;
        public String name;
        public List<Projectile> projectiles;
        public List<Rock> rocks;
        public GameDoc(String name, int width, int height, int top)
        {
            this.name = name;
            player = new Player(new Point(width/2, (height - top)/2));
            projectiles = new List<Projectile>();
            rocks = new List<Rock>();
            initRocks(width, height, top);
        }
        public void addProjectile()
        {
            if(player.lastHorizontal == Direction.Left)
                projectiles.Add(new Projectile(new Point(player.center.X - player.width / 2, player.center.Y - 12), player.lastHorizontal));
            else
                projectiles.Add(new Projectile(new Point(player.center.X + player.width / 2, player.center.Y - 12), player.lastHorizontal));
        }
        public void moveProjectiles(int width, int height, int top)
        {
            for(int i=0; i < projectiles.Count; i++)
            {
                if (!projectiles.ElementAt(i).Move(width, height, top))
                    projectiles.RemoveAt(i);

            }
        }
        public void drawProjectiles(Graphics g)
        {
            foreach (Projectile p in projectiles)
            {
                p.Draw(g);
            }
        }
        public void initRocks(int width, int height, int top)
        {
            Random r = new Random();
            for(int i=0; i<8; i++)
            {
                bool flag = true;
                int x = r.Next(Rock.width, width-Rock.width);
                int y = r.Next(top + Rock.width, height-Rock.width);
                Point p = new Point(x, y);
                foreach (Rock ro in rocks)
                {
                    if (ro.Touches(p, Rock.width, Rock.height))
                    {
                        flag = false;
                        i--;
                        break;
                    }
                }
                if (flag)
                {
                    Rock rock = new Rock(p);
                    if(!rock.Touches(player.center, player.width, player.height))
                        rocks.Add(new Rock(p));
                    else
                    {
                        i--;
                    }
                }
                
            }
        }
        public void drawRocks(Graphics g)
        {
            foreach (Rock r in rocks)
                r.Draw(g);
        }
    }
}
