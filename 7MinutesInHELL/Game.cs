﻿using System;
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
using AxWMPLib;
using System.Media;

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
        public SoundPlayer pewplayer;
        Random r;

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
            timer2.Start();
            FileName = null;
            flagUp = false;
            flagDown = false;
            flagLeft = false;
            flagRight = false;
            r = new Random();
            gd.time = lblTime.Text;
            gd.addDemon(this.Width, this.Height, r);
            axwmp.URL = Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources\\09 - SsSsSsSsSsSsSsSsSs - ingame.mp3");
            axwmp.settings.setMode("loop", true);
            axwmp.Ctlcontrols.play();
            pewplayer  = new System.Media.SoundPlayer(Properties.Resources.pew);
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
            timer2.Start();
            FileName = null;
            flagUp = false;
            flagDown = false;
            flagLeft = false;
            flagRight = false;
            r = new Random();
            lblTime.Text = gd.time;
            axwmp.URL = Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources\\09 - SsSsSsSsSsSsSsSsSs - ingame.mp3");
            axwmp.settings.setMode("loop", true);
            axwmp.Ctlcontrols.play();
            pewplayer = new System.Media.SoundPlayer(Properties.Resources.pew);
            Invalidate(true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (flagMove == true)
            {
                bool flag = true;
                Point p;
                if (gd.player.direction == Direction.Left)
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
                if (gd.portal.CollidesPlayer(gd.player)) gameWin();
            }
            lblPoints.Text = (int.Parse(lblPoints.Text) + gd.moveDemons(this.Width, this.Height)).ToString();
            lblPoints.Text = (int.Parse(lblPoints.Text) + gd.moveProjectiles(this.Width, this.Height, pnlStatus.Height)).ToString();
            if (gd.player.alive == false) gameLose();
            if (gd.checkPowerUp())
            {
                gd.player.powerUp = 15;
                pbReload.ForeColor = Color.GreenYellow;
            }
            Invalidate(true);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
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
            gd.drawPowerUps(e.Graphics);
            gd.drawPortal(e.Graphics);
            gd.drawRocks(e.Graphics);
            gd.player.Draw(e.Graphics);
            gd.drawDemons(e.Graphics);
            gd.drawProjectiles(e.Graphics);
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Up)
                {
                    flagUp = false;
                    if (gd.player.direction == Direction.Up)
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
            if (e.KeyCode == Keys.Escape)
            {

                if (!flagPaused)
                {
                    flagPaused = !flagPaused;
                    pnlMenu.Visible = true;
                    timer1.Stop();
                    timer2.Stop();
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                if (gd.player.flagProjectile)
                {
                    gd.addProjectile();
                    gd.player.flagProjectile = false;
                    pbReload.Value = 0;
                    pewplayer.Play();
                }
            }

        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            flagPaused = !flagPaused;
            pnlMenu.Visible = false;
            timer1.Start();
            timer2.Start();
            this.Focus();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            axwmp.Ctlcontrols.stop();
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

        public void gameLose()
        {
            timer1.Stop();
            timer2.Stop();
            GameLose gl = new GameLose(this.Width, this.Height, this.Location, gd.name, int.Parse(lblPoints.Text));
            this.Hide();
            gl.ShowDialog();
            axwmp.Ctlcontrols.stop();
            this.Close();
        }

        public void gameWin()
        {
            timer1.Stop();
            timer2.Stop();
            GameWin gl = new GameWin(this.Width, this.Height, this.Location, gd.name, int.Parse(lblPoints.Text));
            this.Hide();
            gl.ShowDialog();
            axwmp.Ctlcontrols.stop();
            this.Close();

        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            Instructions ins = new Instructions(this.Width, this.Height, this.Location);
            this.Hide();
            ins.ShowDialog();
            this.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblTime.Text = gd.time;
            String[] ss = lblTime.Text.Split(':');
            int mins = int.Parse(ss[0]);
            int secs = int.Parse(ss[1]);
            if (secs == 0)
            {
                if (mins > 0)
                {
                    mins--;
                    secs = 59;
                }
            }
            else
            {
                secs--;
            }
            lblTime.Text = mins.ToString("00") + ":" + secs.ToString("00");
            gd.time = lblTime.Text;
            if (mins == 6)
            {
                if (secs % 5 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                if (secs % 8 == 0)
                    gd.addDemon(this.Width, this.Height, r);
            }
                
            if (mins == 5)
            {
                if (secs % 4 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                if (secs % 6 == 0)
                    gd.addDemon(this.Width, this.Height, r);
            }
                
            if (mins == 4)
            {
                if (secs % 5 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                if (secs % 3 == 0)
                    gd.addDemon(this.Width, this.Height, r);
            }
                
            if (mins == 3)
            {
                if (secs % 4 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                if (secs % 2 == 0)
                    gd.addDemon(this.Width, this.Height, r);
            }

            if (mins == 2)
            {
                if (secs % 3 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                gd.addDemon(this.Width, this.Height, r);
            }

            if (mins == 1)
            {
                if (secs % 2 == 0)
                    gd.addDemon(this.Width, this.Height, r);
                gd.addDemon(this.Width, this.Height, r);
            }

            if (mins == 0)
            {
                gd.addDemon(this.Width, this.Height, r);
                gd.addDemon(this.Width, this.Height, r);

            }

            if(mins == 1 && secs == 0)
            {
                gd.portal.show();
            }
            if (mins == 0 && secs == 30)
                gd.portal.open();
            if (mins == 0 && secs == 0)
                gameLose();

            gd.timePowerUps();
            if (gd.player.timePowerUp())
            {
                if (pbReload.Value >= 50)
                {
                    pbReload.Value = 50;
                }
                pbReload.Maximum = 50;
            }
            else
            {
                pbReload.Maximum = 100;
                pbReload.ForeColor = Color.DarkOrange;
            }
                

            if(secs % 15 == 0)
            {
                if (r.Next(10) < 4)
                {
                    gd.addPowerUp(this.Width, this.Height, pnlStatus.Height, r);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Game save file (*.gsf)|*.gsf";
            openFileDialog.Title = "Open game save file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        gd = (GameDoc)formater.Deserialize(fileStream);
                        lblTime.Text = gd.time;
                        Invalidate(true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName + "\n" + ex.Message);
                    FileName = null;
                    return;
                }
            }
        }
    }
}
