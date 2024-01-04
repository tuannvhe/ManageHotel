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
    public partial class RegisterGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public RegisterGUI()
        {
            InitializeComponent();
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
        private string code;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            User user = db.Users.Where(u => u.Username == txtUsername.Text).SingleOrDefault();
            if (txtUsername.Text.Length == 0 ||
                txtPassword.Text.Length == 0 ||
                txtRepassword.Text.Length == 0 ||
                txtFirstName.Text.Length == 0 ||
                txtLastName.Text.Length == 0 ||
                txtPhone.Text.Length == 0 ||
                txtEmail.Text.Length == 0)
            {
                MessageBox.Show("You must fill all information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (user!=null)
            {
                MessageBox.Show("Username is already exist!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
            }
            else if (!txtPassword.Text.Equals(txtRepassword.Text))
            {
                MessageBox.Show("Your password and re password does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else if (txtPhone.Text.Length != 10 || !isNumber())
            {
                MessageBox.Show("Phone number must be digits and 10 digits!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
            }
            else if (!IsEmail(txtEmail.Text))
            {
                MessageBox.Show("Your email is not valid!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else if (code == null)
            {
                MessageBox.Show("You have not sent the confirmation code to the email!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtCode.Text.Length == 0)
            {
                MessageBox.Show("You have not entered the code!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!txtCode.Text.Equals(code))
            {
                MessageBox.Show("The code you entered does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCode.Focus();
            }
            else
            {
                User user1 = new User
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Role = 2,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Phone = int.Parse(txtPhone.Text),
                    Email = txtEmail.Text,
                    Money = 0
                };
                try
                {
                    db.Users.Add(user1);
                    db.SaveChanges();
                    MessageBox.Show("Register Success", "Alert", MessageBoxButtons.OK);
                    LoginGUI loginGUI = new LoginGUI();
                    loginGUI.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Register error: " + ex.Message);
                }
            }
        }
        private bool isNumber()
        {
            int n = 0;
            if (int.TryParse(txtPhone.Text, out n))
            {
                return true;
            }
            else return false;
        }
        private bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Do you want to cancel?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                LoginGUI loginGUI = new LoginGUI();
                loginGUI.Show();
                this.Close();
            }
        }     
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                code = RandomString(6, false);
                // tạo một tin nhắn và thêm những thông tin cần thiết như: ai gửi, người nhận, tên tiêu đề, và có đôi lời gì cần nhắn nhủ
                MailMessage mail = new MailMessage("tuannvhe141515@fpt.edu.vn", txtEmail.Text, "Code Reset Password", "Your code: " + code); //
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
                MessageBox.Show("A code has been sent to your email", "Alert", MessageBoxButtons.OK);
            }
            catch (Exception)
            {
                MessageBox.Show("Your email is not valid!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }          
        }
        
    }
}
