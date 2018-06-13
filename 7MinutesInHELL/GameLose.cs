using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7MinutesInHELL
{
    public partial class GameLose : Form
    {
        public GameLose(int width, int height, Point location)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            this.Location = location;
            this.DoubleBuffered = true;
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
