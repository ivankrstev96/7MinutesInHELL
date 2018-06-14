using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _7MinutesInHELL
{
    public partial class LeaderBoards : Form
    {
        public static String path = Path.Combine(System.IO.Path.GetFullPath(@"..\..\"), "Resources") + "\\leaderboards.lb";
        public LeaderBoards(int width, int height, Point location)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            this.Location = location;
            showTop();
        }
        public void showTop()
        {
            TableLayoutPanel tlpLeaderBoards = new TableLayoutPanel();
            this.Controls.Add(tlpLeaderBoards);

            tlpLeaderBoards.AutoSize = true;
            tlpLeaderBoards.Width = this.Width/2;
            tlpLeaderBoards.Location = new Point(this.Width / 4, 50);
            tlpLeaderBoards.BackColor = Color.Black;
            tlpLeaderBoards.ForeColor = Color.DarkRed;

            tlpLeaderBoards.ColumnCount = 3;
            tlpLeaderBoards.RowCount = 1;
            tlpLeaderBoards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLeaderBoards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpLeaderBoards.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Label l = new Label() { Text = "Player" };
            l.Dock = DockStyle.Right;
            l.Font = new Font("Arial", 30, FontStyle.Bold);
            l.AutoSize = true;
            l.TextAlign = ContentAlignment.MiddleRight;
            tlpLeaderBoards.Controls.Add(l, 0, 0);
            l = new Label() { Text = "Points" };
            l.Font = new Font("Arial", 30, FontStyle.Bold);
            l.AutoSize = true;
            l.Dock = DockStyle.Left;
            l.TextAlign = ContentAlignment.MiddleLeft;
            tlpLeaderBoards.Controls.Add(l, 1, 0);
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
            }
            if(lbl != null)
            {
                foreach(playerPoints pp in lbl.getPlayers())
                {
                    tlpLeaderBoards.RowCount += 1;
                    tlpLeaderBoards.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
                    l = new Label() { Text = pp.name };
                    l.TextAlign = ContentAlignment.MiddleRight;
                    l.Font = new Font("Arial", 25, FontStyle.Bold);
                    l.AutoSize = true;
                    l.Dock = DockStyle.Right;
                    tlpLeaderBoards.Controls.Add(l, 0, tlpLeaderBoards.RowCount -1);
                    l = new Label() { Text = pp.points.ToString() };
                    l.Font = new Font("Arial", 25, FontStyle.Bold);
                    l.AutoSize = true;
                    l.TextAlign = ContentAlignment.MiddleLeft;
                    l.Dock = DockStyle.Left;
                    tlpLeaderBoards.Controls.Add(l, 1, tlpLeaderBoards.RowCount - 1);
                }
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
