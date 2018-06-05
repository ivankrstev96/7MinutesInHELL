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
    public partial class Instructions : Form
    {
        public Instructions(int width, int height, Point location)
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Width = width;
            this.Height = height;
            this.Location = location;
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
