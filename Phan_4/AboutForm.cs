using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phan_4
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        // Set Tool Tip 
        private void AboutForm_Load(object sender, EventArgs e)
        {
            closeToolTip.SetToolTip(closeButton, "Click to close About Form.");
        }

        // Handle close button click event
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
