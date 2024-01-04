
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
    public partial class AddEditHotelGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        private int _id;
        public AddEditHotelGUI()
        {
            InitializeComponent();
        }

        public AddEditHotelGUI(int id, HotelManagerContext db)
        {
            InitializeComponent();
            _id = id;
            this.db = db;
            var province = (from p in db.Provinces select new { p.Id, p.ProvinceName }).ToList();
            cboProvince.DataSource = province;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "Id";
            if (_id != -1)
            {
                try
                {
                    Hotel hotel = db.Hotels.Find(_id);
                    txtName.Text = hotel.HotelName.ToString();
                    cboProvince.SelectedValue = (int)hotel.PId;
                }
                catch (Exception)
                {
                }
            }  
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("You must fill all text!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_id == -1)
                {
                    Hotel hotel = new Hotel
                    {
                        HotelName = txtName.Text,
                        PId = (int)cboProvince.SelectedValue
                    };
                    try
                    {
                        db.Hotels.Add(hotel);
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
                        Hotel hotel = db.Hotels.Find(_id);
                        hotel.HotelName = txtName.Text;
                        hotel.PId = (int)cboProvince.SelectedValue;
                        db.Hotels.Update(hotel);
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
