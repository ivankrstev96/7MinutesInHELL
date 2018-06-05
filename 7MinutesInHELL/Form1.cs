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

namespace _7MinutesInHELL
{
    public partial class Form1 : Form
    {
        public string FileName;
        public Game game;
        public System.Media.SoundPlayer mp;
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            game = null;
            FormBorderStyle = FormBorderStyle.None;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            FileName = null;
        }


        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ChooseName cn = new ChooseName();
            cn.StartPosition = FormStartPosition.Manual;
            cn.Location = new Point(this.Width/2 - cn.Width/2, this.Height/2 - cn.Height/2);
            cn.ShowDialog();
            if(cn.DialogResult == DialogResult.OK)
            {
                game = new Game(cn.name, this.Width, this.Height, this.Location);
                this.Hide();
                game.ShowDialog();
                this.Show();
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
                        GameDoc gd = (GameDoc)formater.Deserialize(fileStream);
                        game = new Game(this.Width, this.Height, this.Location, gd);
                        this.Hide();
                        game.ShowDialog();
                        this.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
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
