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
    public partial class RechargeGUI : Form
    {
        private string username;
        private HotelManagerContext db = new HotelManagerContext();

        public RechargeGUI(string username)
        {
            InitializeComponent();
            this.username = username;
            code = RandomString(6, false);
            txtCaptcha.Text = code;
            var bankName = db.Recharges.Select(b => b.BankName).Distinct().ToList();
            cboBankName.DataSource = bankName;
            cboBankName.Text = "BankName";
            //textBox1.Text = RandomDepositCode(6);
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
        private string RandomDepositCode(int numberRD)
        {
            string randomStr = "";
            try
            {

                int[] myIntArray = new int[numberRD];
                int x;
                //that is to create the random # and add it to uour string
                Random autoRand = new Random();
                for (x = 0; x < numberRD; x++)
                {
                    myIntArray[x] = System.Convert.ToInt32(autoRand.Next(0, 9));
                    randomStr += (myIntArray[x].ToString());
                }
            }
            catch (Exception)
            {
                randomStr = "error";
            }
            return randomStr;
        }
        private bool isNumber(string s)
        {
            int n = 0;
            if (int.TryParse(s, out n))
            {
                return true;
            }
            else return false;
        }
        private string code;
        private void btnRecharge_Click(object sender, EventArgs e)
        {
            User user = db.Users.Where(u => u.Username == username).Single();

            string depositCode = RandomDepositCode(9);

            if (txtBankID.Text.Length == 0 || txtPhone.Text.Length == 0 || txtMoney.Text.Length == 0)
            {
                MessageBox.Show("You must fill all text!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!isNumber(txtBankID.Text))
            {
                MessageBox.Show("Bank ID must be digit!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBankID.Focus();
            }
            else if (txtPhone.Text.Length != 10 || !isNumber(txtPhone.Text))
            {
                MessageBox.Show("Phone number must be digits and 10 digits!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
            }
            else if (!isNumber(txtMoney.Text))
            {
                MessageBox.Show("Money must be digits!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoney.Focus();
            }
            else if (!txtPhone.Text.Equals(user.Phone.ToString()))
            {
                MessageBox.Show("Phone number is not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
            }
            else if (code == null)
            {
                MessageBox.Show("You have not sent the confirmation code to the email!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtCaptcha.Text.Length == 0)
            {
                MessageBox.Show("You have not entered the code!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!txtCaptcha.Text.Equals(code))
            {
                MessageBox.Show("The code you entered does not match!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCaptcha.Focus();
            }           
            else
            {
                try
                {                   
                    user.Money += decimal.Parse(txtMoney.Text);
                    Recharge recharge = new Recharge
                    {
                        BankName = cboBankName.Name,
                        BankId = int.Parse(txtBankID.Text),
                        Phone = int.Parse(txtPhone.Text),
                        DepositCode = int.Parse(depositCode),
                        RechargeValue = decimal.Parse(txtMoney.Text),
                        UId = (int)user.Id
                    };
                    db.Recharges.Add(recharge);
                    db.Users.Update(user);
                    db.SaveChanges();
                    MessageBox.Show("Recharge success", "Alert", MessageBoxButtons.OK);
                    this.Hide();
                    ManageAccountGUI m = new ManageAccountGUI(username);
                    m.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            code = RandomString(6, false);
            txtCaptcha.Text = code;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ManageAccountGUI m = new ManageAccountGUI(username);
            m.Show();
            this.Hide();
        }
    }
}
