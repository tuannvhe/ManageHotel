using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManager.GUI
{
    public partial class MainGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        private string _username;
        public MainGUI(string username)
        {
            InitializeComponent();
            _username = username;
            this.Text = "Hello " + _username;
            MainHotel m = new MainHotel(_username);
            m.TopLevel = false;
            m.FormBorderStyle = FormBorderStyle.None;
            m.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(m);
            toolStripContainer1.Font = new Font(toolStripContainer1.Font, FontStyle.Regular);
        }

        private void btnHotel_Click(object sender, EventArgs e)
        {
            MainHotel m = new MainHotel(_username);
            m.TopLevel = false;
            m.FormBorderStyle = FormBorderStyle.None;
            m.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(m);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            MainService m = new MainService(_username);
            m.TopLevel = false;
            m.FormBorderStyle = FormBorderStyle.None;
            m.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(m);
        }

        private void manageMyAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageAccountGUI m = new ManageAccountGUI(_username);
            m.Show();
            this.Hide();
        }

        private void rechargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RechargeGUI g = new RechargeGUI(_username);
            g.Show();
            this.Hide();
        }

        private void lOGOUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to logout?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                this.Hide();
                LoginGUI login = new LoginGUI();
                login.Show();
            }
        }
    }
}
