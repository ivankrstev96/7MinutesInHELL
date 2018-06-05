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
    public partial class ChooseName : Form
    {
        public String name;
        public ChooseName()
        {
            InitializeComponent();
            name = null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            name = txtName.Text;
            DialogResult = DialogResult.OK;
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (txtName.Text.Length <= 0)
            {
                errorProvider1.SetError(txtName, "Enter your name");
                e.Cancel = true;
            }
            else
            {
                if (txtName.Text.Length > 15)
                {
                    errorProvider1.SetError(txtName, "Name can not be longer than 15 characters");
                    e.Cancel = true;
                }
                else
                {
                    errorProvider1.SetError(txtName, null);
                    e.Cancel = false;
                }
            }
        }
    }
}
