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
    public partial class AddEditProvinceGUI : Form
    {
        private int _id;
        private HotelManagerContext db = new HotelManagerContext();

        public AddEditProvinceGUI()
        {
            InitializeComponent();
        }

        public AddEditProvinceGUI(int id, HotelManagerContext db)
        {
            _id = id;
            this.db = db;
            InitializeComponent();
            if (_id != -1)
            {
                try
                {
                    Province province = db.Provinces.Find(_id);
                    txtName.Text = province.ProvinceName.ToString();
                    txtAddress.Text = province.Address.ToString();
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAddress.Text.Length == 0 || txtName.Text.Length == 0)
            {
                MessageBox.Show("You must fill all text!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_id == -1)
                {
                    Province province = new Province
                    {
                        ProvinceName = txtName.Text,
                        Address = txtAddress.Text
                    };
                    try
                    {
                        db.Provinces.Add(province);
                        db.SaveChanges();
                        MessageBox.Show("Add success!");
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Failled!");
                    }
                }
                else
                {

                    try
                    {
                        Province province = db.Provinces.Find(_id);
                        
                            province.ProvinceName = txtName.Text;
                            province.Address = txtAddress.Text;
                            db.Provinces.Update(province);
                            db.SaveChanges();
                            MessageBox.Show("Edit success!");
                            this.Close();
                                              
                    }
                    catch
                    {
                        MessageBox.Show("Failled!");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
