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
    public partial class RoomGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public RoomGUI()
        {
            InitializeComponent();
            var province = (from p in db.Provinces select new { p.Id, p.ProvinceName }).ToList();
            cboProvince.DataSource = province;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "Id";
            var hotel = (from h in db.Hotels select new { h.Id, h.HotelName, h.PId }).Where(h => h.PId == (int)cboProvince.SelectedValue).ToList();
            cboHotel.DataSource = hotel;
            cboHotel.DisplayMember = "HotelName";
            cboHotel.ValueMember = "Id";
            loadData();
        }
        private void loadData()
        {
            dtgRoom.Columns.Clear();
            try
            {
                var room = (from r in db.Rooms select new { r.Id, r.RoomName, r.Status, r.RoomType, r.Price, r.HId, r.HIdNavigation.HotelName }).Where(r => r.HId == (int)cboHotel.SelectedValue).ToList();
                dtgRoom.DataSource = room;
                dtgRoom.Columns["Id"].Visible = false;
                dtgRoom.Columns["HId"].Visible = false;

                int countC = dtgRoom.Columns.Count;
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
                {
                    Text = "Edit",
                    Name = "Edit",
                    UseColumnTextForButtonValue = true
                };
                dtgRoom.Columns.Insert(countC, btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
                {
                    Text = "Delete",
                    Name = "Delete",
                    UseColumnTextForButtonValue = true
                };
                dtgRoom.Columns.Insert(countC + 1, btnDelete);
            }
            catch (Exception)
            {
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

        private void cboHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditRoomGUI a = new AddEditRoomGUI(-1, db);
            a.ShowDialog(this);
            loadData();
        }

        private void dtgRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgRoom.Columns["Delete"].Index)
                {
                    int id = (int)dtgRoom.Rows[e.RowIndex].Cells["Id"].Value;
                    DialogResult dr = MessageBox.Show("Do you want to delete?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            Room room = db.Rooms.Find(id);
                            db.Rooms.Remove(room);
                            db.SaveChanges();
                            MessageBox.Show("Delete success");
                            loadData();
                        }
                        catch
                        {
                            MessageBox.Show("Failled!");
                        }
                    }
                }
                if (e.ColumnIndex == dtgRoom.Columns["Edit"].Index)
                {
                    int id = (int)dtgRoom.Rows[e.RowIndex].Cells["Id"].Value;
                    AddEditRoomGUI a = new AddEditRoomGUI(id, db);
                    a.ShowDialog(this);
                    loadData();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
