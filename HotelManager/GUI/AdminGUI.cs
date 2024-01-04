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
    public partial class AdminGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        private string _username;
        public AdminGUI()
        {
            InitializeComponent();        
        }
        
        public AdminGUI(string username)
        {
            _username = username;
            InitializeComponent();
            label.Text = "Hello " + _username;
        }

        private void AdminGUI_Load(object sender, EventArgs e)
        {

        }

        private void provinceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProvinceGUI p = new ProvinceGUI();
            p.TopLevel = false;
            p.FormBorderStyle = FormBorderStyle.None;
            p.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(p);
        }

        private void loadRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoomGUI r = new RoomGUI();
            r.TopLevel = false;
            r.FormBorderStyle = FormBorderStyle.None;
            r.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(r);
        }

        private void loadHotelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HotelGUI h = new HotelGUI();
            h.TopLevel = false;
            h.FormBorderStyle = FormBorderStyle.None;
            h.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(h);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to logout?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                this.Hide();
                LoginGUI login = new LoginGUI();
                login.Show();
            }            
        }

        private void serviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceGUI s = new ServiceGUI();
            s.TopLevel = false;
            s.FormBorderStyle = FormBorderStyle.None;
            s.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(s);
        }

        private void allOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllOrderGUI b = new AllOrderGUI();
            b.TopLevel = false;
            b.FormBorderStyle = FormBorderStyle.None;
            b.Show();
            toolStripContainer1.ContentPanel.Controls.Clear();
            toolStripContainer1.ContentPanel.Controls.Add(b);
        }
    }
}
