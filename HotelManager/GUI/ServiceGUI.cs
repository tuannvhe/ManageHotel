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
    public partial class ServiceGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public ServiceGUI()
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
            dtgService.Columns.Clear();
            try
            {
                var service = (from s in db.Services select new { s.Id, s.ServiceName, s.Price, s.Type, s.Image, s.HId, s.HIdNavigation.HotelName }).Where(s => s.HId == (int)cboHotel.SelectedValue).ToList();
                dtgService.DataSource = service;
                dtgService.Columns["Id"].Visible = false;
                dtgService.Columns["HId"].Visible = false;
                dtgService.Columns["Image"].Visible = false;
                int countC = dtgService.Columns.Count;
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
                {
                    Text = "Edit",
                    Name = "Edit",
                    UseColumnTextForButtonValue = true
                };
                dtgService.Columns.Insert(countC, btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
                {
                    Text = "Delete",
                    Name = "Delete",
                    UseColumnTextForButtonValue = true
                };
                dtgService.Columns.Insert(countC + 1, btnDelete);
                string imageLocation = "";
                imageLocation = dtgService.Rows[0].Cells["Image"].FormattedValue.ToString();
                pictureBox.ImageLocation = imageLocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
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

        private void dtgService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            try
            {
                string imageLocation = "";
                imageLocation = dtgService.Rows[e.RowIndex].Cells["Image"].FormattedValue.ToString();
                pictureBox.ImageLocation = imageLocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                if (e.ColumnIndex == dtgService.Columns["Delete"].Index)
                {
                    int id = (int)dtgService.Rows[e.RowIndex].Cells["Id"].Value;
                    DialogResult dr = MessageBox.Show("Do you want to delete?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            Service service = db.Services.Find(id);
                            db.Services.Remove(service);
                            db.SaveChanges();
                            MessageBox.Show("Delete success");
                             loadData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                if (e.ColumnIndex == dtgService.Columns["Edit"].Index)
                {
                    int id = (int)dtgService.Rows[e.RowIndex].Cells["Id"].Value;
                    AddEditServiceGUI a = new AddEditServiceGUI(id, db);
                    a.ShowDialog(this);
                    loadData();
                }
            }
            catch (Exception)
            {
            }
        }

        private void ServiceGUI_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditServiceGUI a = new AddEditServiceGUI(-1, db);
            a.ShowDialog(this);
            loadData();
        }
    }
}
