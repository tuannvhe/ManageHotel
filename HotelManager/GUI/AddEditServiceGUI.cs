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
    public partial class AddEditServiceGUI : Form
    {
        private int _id;
        private HotelManagerContext db = new HotelManagerContext();

        public AddEditServiceGUI()
        {
            InitializeComponent();
        }

        public AddEditServiceGUI(int id, HotelManagerContext db)
        {
            InitializeComponent();
            _id = id;
            this.db = db;
            var province = (from p in db.Provinces select new { p.Id, p.ProvinceName }).ToList();
            cboProvince.DataSource = province;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "Id";

            var hotel = (from h in db.Hotels select new { h.Id, h.HotelName, h.PId }).Where(h => h.PId == (int)cboProvince.SelectedValue).ToList();
            cboHotel.DataSource = hotel;
            cboHotel.DisplayMember = "HotelName";
            cboHotel.ValueMember = "Id";

            var services = db.Services.GroupBy(s => s.Type).Select(g => new {Type = g.Key }).ToList();
            cboType.DataSource = services;
            cboType.DisplayMember = "Type";
            
            string imageLocation = "";
            imageLocation = txtImage.Text;
            pictureBox.ImageLocation = imageLocation;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            if (_id != -1)
            {
                try
                {
                    Service service = db.Services.Find(_id);
                    Hotel hotels = db.Hotels.Find(service.HId);
                    txtName.Text = service.ServiceName.ToString();
                    txtPrice.Text = service.Price.ToString();
                    cboProvince.SelectedValue = (int)hotels.PId;
                    cboHotel.SelectedValue = (int)service.HId;
                    txtImage.Text = service.Image.ToString();
                    cboType.Text = service.Type.ToString();
                    imageLocation = txtImage.Text;
                    pictureBox.ImageLocation = imageLocation;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception)
                {
                }
            }
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0 || txtPrice.Text.Length == 0 || txtImage.Text.Length == 0)
            {
                MessageBox.Show("You must fill all text!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_id == -1)
                {
                    Service service = new Service
                    {
                        ServiceName = txtName.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        Type = cboType.Text,
                        Image = txtImage.Text,
                        HId = (int)cboHotel.SelectedValue
                    };
                    try
                    {
                        db.Services.Add(service);
                        db.SaveChanges();
                        MessageBox.Show("Add success!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {

                    try
                    {
                        Service service = db.Services.Find(_id);
                        service.ServiceName = txtName.Text;
                        service.HId = (int)cboHotel.SelectedValue;
                        service.Type = cboType.Text;
                        service.Price = decimal.Parse(txtPrice.Text);
                        service.Image = txtImage.Text;
                        db.Services.Update(service);
                        db.SaveChanges();
                        MessageBox.Show("Edit success!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private string urlImage;
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                string imageLocation = "";

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    pictureBox.ImageLocation = imageLocation;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    urlImage = imageLocation;
                    txtImage.Clear();
                    txtImage.Text = imageLocation;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var hotel = (from h in db.Hotels select new { h.Id, h.HotelName, h.PId }).Where(h => h.PId == (int)cboProvince.SelectedValue).ToList();
                cboHotel.DataSource = hotel;
                cboHotel.DisplayMember = "HotelName";
                cboProvince.ValueMember = "Id";
            }
            catch (Exception)
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
