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
    public partial class LoginGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public LoginGUI()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0 || txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Username or password cannt empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    User user = db.Users.SingleOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);
                    if (user != null && user.Role == 1)
                    {
                        AdminGUI adminGUI = new AdminGUI(user.Username.ToString());
                        adminGUI.Show();
                        this.Hide();
                    }
                    else if (user!= null && user.Role == 2)
                    {                      
                        MainGUI mainGUI = new MainGUI(user.Username.ToString());
                        mainGUI.Show();
                        this.Hide();                    
                    }
                    else
                    {
                        MessageBox.Show("Your username or password is not incorrect!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Login error: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to cancel?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterGUI register = new RegisterGUI();
            register.Show();
            this.Hide();
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.White;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.AntiqueWhite;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.AntiqueWhite;
        }

        private void lblForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RecoverForgotPasswordGUI forgotPasswordGUI = new RecoverForgotPasswordGUI();
            forgotPasswordGUI.Show();
            this.Hide();
        }
    }
}
