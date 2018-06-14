using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _7MinutesInHELL
{
    public partial class GameWin : Form
    {
        public static String path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources") + "\\leaderboards.lb";
        public GameWin(int width, int height, Point location, String name, int points)
        {
            InitializeComponent();
            this.Select();
            this.Width = width;
            this.Height = height;
            this.Location = location;
            this.DoubleBuffered = true;
            addToLeaderBoards(name, points + 10000);
            lblPoints.Text = "Score: " + (points + 10000);
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void addToLeaderBoards(String name, int points)
        {
            LeaderBoardsList lbl = null;
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open))
                {
                    IFormatter formater = new BinaryFormatter();
                    lbl = (LeaderBoardsList)formater.Deserialize(fileStream);
                    Invalidate(true);
                }
            }
            catch (Exception ex)
            {
                if (lbl == null)
                    lbl = new LeaderBoardsList();
            }
            //--------------------
            lbl.addNew(name, points);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, lbl);
            }
        }
    }
}
