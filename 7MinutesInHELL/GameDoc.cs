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
    public class GameDoc
    {
        public Player player;
        public String name;
        public Portal portal;
        public List<Projectile> projectiles;
        public List<Rock> rocks;
        public List<Demon> demons;
        public List<PowerUp> powerUps;
        public String time;
        public GameDoc(String name, int width, int height, int top)
        {
            this.name = name;
            player = new Player(new Point(width/2, (height - top)/2));
            projectiles = new List<Projectile>();
            rocks = new List<Rock>();
            demons = new List<Demon>();
            powerUps = new List<PowerUp>();
            init(width, height, top);
        }
        public void drawPortal(Graphics g)
        {
            portal.Draw(g);
        }
        public void addPowerUp(int width, int height, int top, Random r)
        {
            while (true)
            {
                bool flag = true;
                int x = r.Next(PowerUp.width, width - PowerUp.width);
                int y = r.Next(top + PowerUp.width, height - PowerUp.width);
                Point p = new Point(x, y);
                foreach (Rock ro in rocks)
                {
                    if (ro.Touches(p, Rock.width, Rock.height))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    powerUps.Add(new PowerUp(p));
                    break;
                }
            }
        }
        public void drawPowerUps(Graphics g)
        {
            foreach (PowerUp p in powerUps)
                p.Draw(g);
        }
        public void timePowerUps()
        {
            for (int i = 0; i < powerUps.Count; i++)
            {
                if (powerUps.ElementAt(i).reduceTime())
                {
                    powerUps.RemoveAt(i);
                }
            }
        }
        public bool checkPowerUp()
        {
            bool flag = false;
            for(int i=0; i<powerUps.Count; i++)
            {
                if (powerUps.ElementAt(i).CollidesPlayer(player))
                {
                    powerUps.RemoveAt(i);
                    flag = true;
                }
            }
            return flag;
        }
        public int moveDemons(int right, int bottom)
        {
            int ret = 0;
            for(int i=0; i<demons.Count; i++)
            {
                if (demons.ElementAt(i).CollidesPlayer(player))
                {
                    player.alive = false;
                }
                if (!demons.ElementAt(i).Move(right, bottom))
                {
                    demons.RemoveAt(i);
                    ret += 10;
                } 
            }
            return ret;
        }
        public void addDemon(int right, int bottom, Random r)
        {
            
            int pom = r.Next(2);
            Direction direction;
            Bitmap demonce;
            int y = r.Next(40 + Demon.height / 2, bottom - Demon.height / 2);
            if (pom == 0)
            {
                direction = Direction.Left;
                demonce = Properties.Resources.demonce;
                demonce.RotateFlip(RotateFlipType.RotateNoneFlipX);
                demons.Add(new Demon(new Point(right, y), direction, demonce));
            }
            else
            {
                direction = Direction.Right;
                demonce = Properties.Resources.demonce;
                demons.Add(new Demon(new Point(0, y), direction, demonce));
            }
        }

        public void addProjectile()
        {
            if(player.lastHorizontal == Direction.Left)
                projectiles.Add(new Projectile(new Point(player.center.X - player.width / 2, player.center.Y - 12), player.lastHorizontal));
            else
                projectiles.Add(new Projectile(new Point(player.center.X + player.width / 2, player.center.Y - 12), player.lastHorizontal));
        }
        public int moveProjectiles(int width, int height, int top)
        {
            int ret = 0;
            for(int i=0; i < projectiles.Count; i++)
            {
                if (!projectiles.ElementAt(i).Move(width, height, top))
                {
                    projectiles.RemoveAt(i);
                    continue;
                }
                bool flagProjectile = true;
                for(int j=0; j<demons.Count; j++)
                {
                    if (demons.ElementAt(j).isHit(projectiles.ElementAt(i)))
                    {
                        projectiles.RemoveAt(i);
                        demons.RemoveAt(j);
                        flagProjectile = false;
                        ret += 100;
                        break;
                    }
                }
                if (!flagProjectile) continue;
                foreach (Rock r in rocks)
                {
                    if (r.isHit(projectiles.ElementAt(i)))
                    {
                        projectiles.RemoveAt(i);
                        break;
                    }
                        
                }
            }
            return ret;
        }
        public void drawProjectiles(Graphics g)
        {
            foreach (Projectile p in projectiles)
            {
                p.Draw(g);
            }
        }
        public void drawDemons(Graphics g)
        {
            foreach (Demon d in demons)
            {
                d.Draw(g);
            }
        }
        public void init(int width, int height, int top)
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
            while (true)
            {
                int x = r.Next(Portal.width, width - Portal.width);
                int y = r.Next(top + Portal.width, height - Portal.width);
                Point p = new Point(x, y);
                bool flag = true;
                foreach (Rock ro in rocks)
                {
                    if (ro.Touches(p, Rock.width, Rock.height))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    portal = new Portal(p);
                    break;
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
