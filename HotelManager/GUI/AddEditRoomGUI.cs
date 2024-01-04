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
    public partial class AddEditRoomGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        private int _id;

        public AddEditRoomGUI()
        {
            InitializeComponent();
        }

        public AddEditRoomGUI(int id, HotelManagerContext db)
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
            if (_id != -1)
            {
                try
                {
                    txtStatus.Visible = true;
                    lblStt.Visible = true;
                    
                    Room rooms = db.Rooms.Find(_id);
                    Hotel hotels = db.Hotels.Find(rooms.HId);
                    txtName.Text = rooms.RoomName;
                    txtType.Text = rooms.RoomType;
                    txtStatus.Text = rooms.Status;
                    //txtUId.Text = rooms.UId.ToString();
                    txtPrice.Text = Math.Round(decimal.Parse(rooms.Price.ToString()), 0).ToString();
                    cboProvince.SelectedValue = (int)hotels.PId;
                    cboHotel.SelectedValue = (int)rooms.HId;
                }
                catch (Exception)
                {
                }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Length == 0 || txtPrice.Text.Length == 0 || txtType.Text.Length == 0 || txtType.Text.Length == 0 || txtType.Text.Length == 0)
            {
                MessageBox.Show("You enter all information!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!isNumber())
            {
                MessageBox.Show("Price must be digit!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (_id == -1)
                {
                    Room room = new Room
                    {
                        RoomName = txtName.Text,
                        Status = "Available",
                        RoomType = txtType.Text.ToUpper(),
                        HId = (int)cboHotel.SelectedValue,
                        Price = decimal.Parse(txtPrice.Text)
                    };
                    try
                    {
                        db.Rooms.Add(room);
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
                        Room room = db.Rooms.Find(_id);
                        room.RoomName = txtName.Text;
                        room.HId = (int)cboHotel.SelectedValue;
                        room.Status = txtStatus.Text;
                        //room.UId = int.Parse(txtUId.Text);
                        room.RoomType = txtType.Text.ToUpper();
                        room.Price = decimal.Parse(txtPrice.Text);
                        db.Rooms.Update(room);
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
        private bool isNumber()
        {
            int n = 0;
            if (int.TryParse(txtPrice.Text, out n))
            {
                return true;
            }
            else return false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
