using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManager.GUI
{
    public partial class CheckCodeEmailGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        private string username;
        private string password;
        private string code;

        public CheckCodeEmailGUI(string username, string password, string code)
        {
            InitializeComponent();
            this.username = username;
            this.password = password;
            this.code = code;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtCodeMail.Text.Length == 0)
            {
                MessageBox.Show("You have not entered the code", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(!code.Equals(txtCodeMail.Text))
            {
                MessageBox.Show("The code you entered does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodeMail.Focus();
            }
            else
            {
                User user = db.Users.Where(u => u.Username == username).Single();
                try
                {
                    user.Password = password;
                    db.Users.Update(user);
                    db.SaveChanges();
                    MessageBox.Show("Reset password success!");
                    this.Close();
                    LoginGUI loginGUI = new LoginGUI();
                    loginGUI.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Change pass error: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecoverForgotPasswordGUI f = new RecoverForgotPasswordGUI();
            f.Show();
        }
    }
}
