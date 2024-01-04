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
using System.Net;
using System.IO;
using System.Diagnostics;

namespace HotelManager.GUI
{
    public partial class RecoverForgotPasswordGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public RecoverForgotPasswordGUI()
        {
            InitializeComponent();
            txtCaptcha.Text = RandomString(6, false);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0
                || txtPassword.Text.Length == 0
                || txtRePassword.Text.Length == 0
                || txtReCaptcha.Text.Length == 0
                || txtEmail.Text.Length == 0
                || txtPhone.Text.Length == 0)
            {
                MessageBox.Show("You must fill all text!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                User users = db.Users.SingleOrDefault(u => u.Username == txtUsername.Text);
                if (users == null)
                {
                    MessageBox.Show("Your username does not exist!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    try
                    {
                        User user = db.Users.SingleOrDefault(u => u.Username == txtUsername.Text && u.Email == txtEmail.Text && u.Phone == int.Parse(txtPhone.Text));
                        if (user == null)
                        {
                            MessageBox.Show("Your information are not correct!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (!txtPassword.Text.Equals(txtRePassword.Text))
                        {
                            MessageBox.Show("Your new password and re new password does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPassword.Focus();
                        }
                        else if (!txtReCaptcha.Text.ToLower().Equals(txtCaptcha.Text.ToLower()))
                        {
                            txtReCaptcha.Focus();
                            MessageBox.Show("You input wrong captcha!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {               
                            string code = RandomString(6, false);
                            // tạo một tin nhắn và thêm những thông tin cần thiết như: ai gửi, người nhận, tên tiêu đề, và có đôi lời gì cần nhắn nhủ
                            MailMessage mail = new MailMessage("tuannvhe141515@fpt.edu.vn", user.Email, "Code Reset Password", "Your code: " + code); //
                            mail.IsBodyHtml = true;
                            //gửi tin nhắn
                            SmtpClient client = new SmtpClient(code);
                            client.Host = "smtp.gmail.com";
                            //ta không dùng cái mặc định đâu, mà sẽ dùng cái của riêng mình
                            client.UseDefaultCredentials = false;
                            client.Port = 587; //vì sử dụng Gmail nên mình dùng port 587
                                               // thêm vào credential vì SMTP server cần nó để biết được email + password của email đó mà bạn đang dùng
                            client.Credentials = new System.Net.NetworkCredential("tuannvhe141515@fpt.edu.vn", "Tuanbgls1");
                            client.EnableSsl = true; //vì ta cần thiết lập kết nối SSL với SMTP server nên cần gán nó bằng true
                            client.Send(mail);
                            DialogResult dlr = MessageBox.Show("A code has been sent to your email", "Alert", MessageBoxButtons.OK);
                            try
                            {                               
                                if (dlr == DialogResult.OK)
                                {
                                
                                    CheckCodeEmailGUI c = new CheckCodeEmailGUI(user.Username, txtPassword.Text, code);
                                    c.Show();
                                    this.Hide();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                                                       
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Reset pass error: " + ex.Message);
                    }
                }
            }           
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder stringBuilder = new StringBuilder();
            char c;
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                stringBuilder.Append(c);
            }
            if (lowerCase)
                return stringBuilder.ToString().ToLower();
            return stringBuilder.ToString();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCaptcha.Text = RandomString(6, false);
            txtReCaptcha.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to cancel?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                this.Close();
                LoginGUI loginGUI = new LoginGUI();
                loginGUI.Show();
            }
        }
    }
}
