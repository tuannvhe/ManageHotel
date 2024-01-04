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
    public partial class ManageAccountGUI : Form
    {
        private string _username;
        HotelManagerContext db = new HotelManagerContext();

        public ManageAccountGUI(string username)
        {
            InitializeComponent();
            _username = username;
            LoadData();          
        }
        private void LoadData()
        {
            try
            {
                User user = db.Users.Where(u => u.Username == _username).Single();
                txtEmail.Text = user.Email;
                txtFirst.Text = user.FirstName;
                txtLast.Text = user.LastName;
                txtPhone.Text = user.Phone.ToString();
                txtMoney.Text = Math.Round(decimal.Parse(user.Money.ToString()),0) + " VNĐ";
                var order = db.Orders.Where(o => o.UId == user.Id).ToList();
                dtgView.DataSource = order;
                dtgView.Columns["Id"].Visible = false;               
                dtgView.Columns["UId"].Visible = false;
                dtgView.Columns["UIdNavigation"].Visible = false;
                dtgView.Columns["OrderRooms"].Visible = false;
                dtgView.Columns["OrderServices"].Visible = false;
                LoadBooked();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void LoadBooked()
        {
            dtgBooked.Columns.Clear();
            User user = db.Users.Where(u => u.Username == _username).Single();
            
            var order = db.Orders.Where(o => o.UId == user.Id && o.Status == "InUse").SingleOrDefault();
            if (order!=null)
            {
                var orderRoom = db.OrderRooms.Where(o => o.OId == order.Id).ToList();

                var roomBooked = from r1 in db.Rooms
                                 join r2 in db.OrderRooms.Where(o => o.OId == order.Id)
                                 on r1.Id equals r2.RId
                                 join r3 in db.Hotels
                                 on r1.HId equals r3.Id
                                 select new
                                 {
                                     r1.Id,
                                     r1.RoomName,
                                     r1.Price,
                                     r1.RoomType,
                                     r3.HotelName
                                 };
                dtgBooked.DataSource = roomBooked.ToList();
            }
            else
            {
                dtgBooked.Columns.Clear();
            }          
            
            int countC = dtgBooked.Columns.Count;
            DataGridViewButtonColumn btnCheckout = new DataGridViewButtonColumn
            {
                Text = "Checkout",
                Name = "Checkout",
                UseColumnTextForButtonValue = true
            };
            dtgBooked.Columns.Insert(countC, btnCheckout);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainGUI m = new MainGUI(_username);
            m.Show();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(u => u.Username == _username).Single();
            if (txtOld.Text.Length == 0 || txtNew.Text.Length == 0 || txtReNew.Text.Length == 0)
            {
                txtOld.Focus();
                MessageBox.Show("You must input old password, new password and re-new password!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!txtOld.Text.Equals(user.Password))
            {
                txtOld.Focus();
                MessageBox.Show("Your old password does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }else if (!txtNew.Text.Equals(txtReNew.Text))
            {
                txtNew.Focus();
                MessageBox.Show("New password and re-new password does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                user.Password = txtReNew.Text;
                db.Users.Update(user);
                db.SaveChanges();
                MessageBox.Show("Change password success!", "Alert", MessageBoxButtons.OK);
                txtOld.Text = "";
                txtNew.Text = "";
                txtReNew.Text = "";
                LoadData();
            }
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            if (txtFirst.Text.Length == 0 || txtLast.Text.Length == 0)
            {
                txtFirst.Focus();
                MessageBox.Show("New first name or last name can not empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var user = db.Users.Where(u => u.Username == _username).Single();
                user.FirstName = txtFirst.Text;
                user.LastName = txtLast.Text;
                db.Users.Update(user);
                db.SaveChanges();
                MessageBox.Show("Change name success!", "Alert", MessageBoxButtons.OK);
                LoadData();
            }           
        }

        private void btnChangePhone_Click(object sender, EventArgs e)
        {
            if (!isNumber())
            {
                MessageBox.Show("Phone number must be digit!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPhone.Text.Length != 10)
            {
                MessageBox.Show("Phone number must be digits and 10 digits!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
            }
            else
            {
                var user = db.Users.Where(u => u.Username == _username).Single();
                user.Phone = int.Parse(txtPhone.Text);
                db.Users.Update(user);
                db.SaveChanges();
                MessageBox.Show("Change phone number success!","Alert",MessageBoxButtons.OK);
                LoadData();
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
        private string code;
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
        private void btnChangeEmail_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(u => u.Username == _username).Single();

            if (txtEmail.Text.Length==0)
            {
                MessageBox.Show("You must input email!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else if (user.Email.Equals(txtEmail.Text))
            {
                MessageBox.Show("Your email is match old email!", "Alert", MessageBoxButtons.OK);
            }
            else if (!IsEmail(txtEmail.Text))
            {
                MessageBox.Show("Your new email is not valid!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
            }
            else
            {
                try
                {
                    code = RandomString(6, false);
                    // tạo một tin nhắn và thêm những thông tin cần thiết như: ai gửi, người nhận, tên tiêu đề, và có đôi lời gì cần nhắn nhủ
                    MailMessage mail = new MailMessage("tuannvhe141515@fpt.edu.vn", txtEmail.Text, "Code Change Your Email", "Your code: " + code); //
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
                    MessageBox.Show("A code has been send to your new email, please check your email and input true code to change email", "Alert", MessageBoxButtons.OK);
                    lblValidCode.Visible = true;
                    txtCode.Visible = true;
                    btnCheck.Visible = true;                  
                }
                catch (Exception)
                {
                    MessageBox.Show("Your email is not valid!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }        
            }
        }

        private void btnRecharge_Click(object sender, EventArgs e)
        {
            RechargeGUI r = new RechargeGUI(_username);
            r.Show();
            this.Hide();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var user = db.Users.Where(u => u.Username == _username).Single();
            if (txtCode.Text.Length == 0)
            {
                MessageBox.Show("You have not entered the code!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!txtCode.Text.Equals(code))
            {
                MessageBox.Show("The code you entered does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                user.Email = txtEmail.Text;
                db.Users.Update(user);
                db.SaveChanges();
                MessageBox.Show("Change email success!", "Alert", MessageBoxButtons.OK);
                LoadData();
                lblValidCode.Visible = false;
                txtCode.Visible = false;
                btnChangeEmail.Visible = false;
                lblValidCode.Text = "";
                txtCode.Text = "";               
            }
        } 

        private void dtgBooked_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgBooked.Columns["Checkout"].Index)
                {
                    int id = (int)dtgBooked.Rows[e.RowIndex].Cells["Id"].Value;
                    DialogResult dr = MessageBox.Show($"Do you want to checkout?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {

                            User user = db.Users.Where(u => u.Username == _username).Single();

                            var order = db.Orders.Where(o => o.UId == user.Id 
                            && o.Status == "InUse").Single();

                            if (dtgBooked.Rows.Count == 1)
                            {
                                dtgBooked.Columns.Clear();
                                order.Status = "Checkout";
                                db.Orders.Update(order);
                                db.SaveChanges();
                                DateTime From = (DateTime)order.DateBook;
                                DateTime To = DateTime.Now;

                                TimeSpan ts = To - From;

                                string[] s = ts.ToString().Split(".");
                                string totalDay = Convert.ToString(s[0]);

                                string[] s1 = order.DateBook.ToString().Split(" ");
                                string today = Convert.ToString(s1[0]);

                                string[] s2 = DateTime.Now.ToString().Split(" ");
                                string now = Convert.ToString(s2[0]);

                                Room room = db.Rooms.Find(id);
                                if (today.Equals(now))
                                {
                                    //order.Status = "Checkout";
                                    var orders = db.OrderRooms.Where(o => o.RId == id).Single();
                                    OrderRoom orderRoom = db.OrderRooms.Find(orders.Id);
                                    room.Status = "Available";
                                    db.Rooms.Update(room);
                                    db.Orders.Update(order);
                                    db.OrderRooms.Remove(orderRoom);
                                    db.SaveChanges();

                                    LoadBooked();
                                    LoadData();
                                    MessageBox.Show($"Checkout this room success.", "Alert", MessageBoxButtons.OK);
                                }
                                else
                                {

                                    order.DateCheckout = DateTime.Now;
                                    var orders = db.OrderRooms.Where(o => o.RId == id).Single();
                                    OrderRoom orderRoom = db.OrderRooms.Find(orders.Id);
                                    room.Status = "Available";

                                    decimal totalMoney = int.Parse(totalDay) * (decimal)room.Price;

                                    if (user.Money < totalMoney)
                                    {
                                        DialogResult dr1 = MessageBox.Show($"Your account not enough{Math.Round(totalMoney, 0)} VNĐ to checkout", "Alert",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                        if (dr == DialogResult.OK)
                                        {
                                            RechargeGUI recharge = new RechargeGUI(_username);
                                            recharge.Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        user.Money = user.Money - totalMoney;
                                        order.Total += totalMoney;
                                        db.Orders.Update(order);
                                        db.Rooms.Update(room);
                                        db.Users.Update(user);
                                        db.OrderRooms.Remove(orderRoom);
                                        db.SaveChanges();
                                        LoadBooked();
                                        LoadData();
                                        MessageBox.Show($"Checkout success with price {Math.Round(totalMoney, 0)} VNĐ for {totalDay} days.", "Alert", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            else
                            {
                                DateTime From = (DateTime)order.DateBook;
                                DateTime To = DateTime.Now;

                                TimeSpan ts = To - From;

                                string[] s = ts.ToString().Split(".");
                                string totalDay = Convert.ToString(s[0]);

                                string[] s1 = order.DateBook.ToString().Split(" ");
                                string today = Convert.ToString(s1[0]);

                                string[] s2 = DateTime.Now.ToString().Split(" ");
                                string now = Convert.ToString(s2[0]);

                                Room room = db.Rooms.Find(id);
                                if (today.Equals(now))
                                {
                                    //order.Status = "Checkout";
                                    var orders = db.OrderRooms.Where(o => o.RId == id).Single();
                                    OrderRoom orderRoom = db.OrderRooms.Find(orders.Id);
                                    room.Status = "Available";
                                    db.Rooms.Update(room);
                                    db.Orders.Update(order);
                                    db.OrderRooms.Remove(orderRoom);
                                    db.SaveChanges();

                                    LoadBooked();
                                    LoadData();
                                    MessageBox.Show($"Checkout this room success.", "Alert", MessageBoxButtons.OK);
                                }
                                else
                                {

                                    order.DateCheckout = DateTime.Now;
                                    var orders = db.OrderRooms.Where(o => o.RId == id).Single();
                                    OrderRoom orderRoom = db.OrderRooms.Find(orders.Id);
                                    room.Status = "Available";

                                    decimal totalMoney = int.Parse(totalDay) * (decimal)room.Price;

                                    if (user.Money < totalMoney)
                                    {
                                        DialogResult dr1 = MessageBox.Show($"Your account not enough{Math.Round(totalMoney, 0)} VNĐ to checkout", "Alert",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                                        if (dr == DialogResult.OK)
                                        {
                                            RechargeGUI recharge = new RechargeGUI(_username);
                                            recharge.Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        user.Money = user.Money - totalMoney;
                                        order.Total += totalMoney;
                                        db.Orders.Update(order);
                                        db.Rooms.Update(room);
                                        db.Users.Update(user);
                                        db.OrderRooms.Remove(orderRoom);
                                        db.SaveChanges();
                                        LoadBooked();
                                        LoadData();
                                        MessageBox.Show($"Checkout success with price {Math.Round(totalMoney, 0)} VNĐ for {totalDay} days.", "Alert", MessageBoxButtons.OK);
                                    }
                                }
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrom.Text.Length == 0 && txtTo.Text.Length == 0)
                {
                    LoadData();
                }
                else
                {
                    User user = db.Users.Where(u => u.Username == _username).Single();

                    dtgView.DataSource = db.Orders.Where(o => o.UId == user.Id
                    && o.DateCheckout >= DateTime.Parse(txtFrom.Text)
                    && o.DateCheckout <= DateTime.Parse(txtTo.Text))
                        .ToList();
                    dtgView.Columns["Id"].Visible = false;
                    dtgView.Columns["UId"].Visible = false;
                    dtgView.Columns["HId"].Visible = false;
                    dtgView.Columns["RId"].Visible = false;
                    dtgView.Columns["UIdNavigation"].Visible = false;
                    dtgView.Columns["OrderServices"].Visible = false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You must input correct format(mm/DD/yyyy)");                
            }
            
        }
    }
}
