using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using _7MinutesInHELL.Properties;

namespace _7MinutesInHELL
{
    [Serializable]
    public partial class Game : Form
    {
        public GameDoc gd;
        public bool flagMove;
        public bool flagUp;
        public bool flagDown;
        public bool flagLeft;
        public bool flagRight;
        public string FileName;
        public bool flagPaused;
        public Game(String name, int width, int height, Point location)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = width;
            this.Height = height;
            this.Location = location;
            flagMove = false;
            flagPaused = false;
            gd = new GameDoc(name, width, height, pnlStatus.Height);
            timer1.Start();
            FileName = null;
            flagUp = false;
            flagDown = false;
            flagLeft = false;
            flagRight = false;
            Invalidate(true);
        }
        public Game(int width, int height, Point location, GameDoc gd)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = width;
            this.Height = height;
            this.Location = location;
            flagMove = false;
            flagPaused = false;
            this.gd = gd;
            timer1.Start();
            FileName = null;
            flagUp = false;
            flagDown = false;
            flagLeft = false;
            flagRight = false;
            Invalidate(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(flagMove == true)
            {
                bool flag = true;
                Point p;
                if(gd.player.direction == Direction.Left)
                {
                    p = new Point(gd.player.center.X - Player.velocity, gd.player.center.Y);
                }
                else if (gd.player.direction == Direction.Right)
                {
                    p = new Point(gd.player.center.X + Player.velocity, gd.player.center.Y);
                }
                else if (gd.player.direction == Direction.Up)
                {
                    p = new Point(gd.player.center.X, gd.player.center.Y - Player.velocity);
                }
                else
                {
                    p = new Point(gd.player.center.X, gd.player.center.Y + Player.velocity);
                }
                foreach (Rock r in gd.rocks)
                {
                    if (r.Colides(p, gd.player.width, gd.player.height))
                    {
                        flag = false;
                        break;
                    }
                        
                }
                if (flag)
                    gd.player.Move(this.Width, this.Height, pnlStatus.Height);
            }
            gd.moveProjectiles(this.Width, this.Height, pnlStatus.Height);
            Invalidate(true);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.KeyCode == Keys.Up)
            {
                flagMove = true;
                flagUp = true;
                gd.player.Turn(Direction.Up);
            }
            if (e.KeyCode == Keys.Down)
            {
                flagMove = true;
                flagDown = true;
                gd.player.Turn(Direction.Down);
            }
            if (e.KeyCode == Keys.Left)
            {
                flagMove = true;
                flagLeft = true;
                gd.player.Turn(Direction.Left);
            }
            if (e.KeyCode == Keys.Right)
            {
                flagMove = true;
                flagRight = true;
                gd.player.Turn(Direction.Right);
            }
            
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            gd.drawProjectiles(e.Graphics);
            gd.drawRocks(e.Graphics);
            gd.player.Draw(e.Graphics);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Up)
                {
                    flagUp = false;
                    if(gd.player.direction == Direction.Up)
                    {
                        flagMove = false;
                        gd.player.flagMoving = false;
                    }
                }
                    
                if (e.KeyCode == Keys.Down)
                {
                    flagDown = false;
                    if (gd.player.direction == Direction.Down)
                    {
                        flagMove = false;
                        gd.player.flagMoving = false;
                    }
                }
                if (e.KeyCode == Keys.Left)
                {
                    flagLeft = false;
                    if (gd.player.direction == Direction.Left)
                    {
                        flagMove = false;
                        gd.player.flagMoving = false;
                    }
                }
                if (e.KeyCode == Keys.Right)
                {
                    flagRight = false;
                    if (gd.player.direction == Direction.Right)
                    {
                        flagMove = false;
                        gd.player.flagMoving = false;
                    }
                } 
                if (!flagUp && !flagDown && !flagLeft && !flagLeft && !flagRight)
                {
                    flagMove = false;
                    gd.player.flagMoving = false;
                }
                
            }
            if(e.KeyCode == Keys.Escape)
            {
                
                if (!flagPaused)
                {
                    flagPaused = !flagPaused;
                    pnlMenu.Visible = true;
                    timer1.Stop();
                }
            }
            if(e.KeyCode == Keys.Space)
            {
                if (gd.player.flagProjectile)
                {
                    gd.addProjectile();
                    gd.player.flagProjectile = false;
                    pbReload.Value = 0;
                }
            }
            
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            flagPaused = !flagPaused;
            pnlMenu.Visible = false;
            timer1.Start();
            this.Focus();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Game save file (*.gsf)|*.gsf";
                saveFileDialog.Title = "Game save file";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }
            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, gd);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (pbReload.Value >= pbReload.Maximum)
            {
                gd.player.flagProjectile = true;
            }
            else
            {
                pbReload.Value = pbReload.Value + pbReload.Step;
            }
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            Instructions ins = new Instructions(this.Width, this.Height, this.Location);
            this.Hide();
            ins.ShowDialog();
            this.Show();
        }
    }
}
