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
    public partial class HotelGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public HotelGUI()
        {
            InitializeComponent();
            var province = (from p in db.Provinces select new { p.Id, p.ProvinceName }).ToList();
            cboProvince.DataSource = province;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "Id";
            loadData();
        }
        private void loadData()
        {
            dtgHotel.Columns.Clear();
            try
            {
                var hotel = db.Hotels
                   .Select(h => new { h.Id, h.HotelName, h.PId, h.PIdNavigation.ProvinceName }).Where( h => h.PId == (int)cboProvince.SelectedValue)
                                .ToList();
                dtgHotel.DataSource = hotel;
                dtgHotel.Columns["Id"].Visible = false;
                dtgHotel.Columns["PId"].Visible = false;
                int countC = dtgHotel.Columns.Count;

                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
                {
                    Text = "Edit",
                    Name = "Edit",
                    UseColumnTextForButtonValue = true
                };                
                dtgHotel.Columns.Insert(countC, btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
                {
                    Text = "Delete",
                    Name = "Delete",                   
                    UseColumnTextForButtonValue = true
                };
                dtgHotel.Columns.Insert(countC + 1, btnDelete);
            }
            catch (Exception)
            {
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditHotelGUI a = new AddEditHotelGUI(-1, db);
            a.ShowDialog(this);
            loadData();
        }

        private void cboProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtgHotel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgHotel.Columns["Delete"].Index)
                {
                    int id = (int)dtgHotel.Rows[e.RowIndex].Cells["Id"].Value;
                    DialogResult dr = MessageBox.Show("Do you want to delete?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {
                            Hotel hotel = db.Hotels.Find(id);                                                    
                            db.Hotels.Remove(hotel);
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
                if (e.ColumnIndex == dtgHotel.Columns["Edit"].Index)
                {
                    int id = (int)dtgHotel.Rows[e.RowIndex].Cells["Id"].Value;
                    AddEditHotelGUI a = new AddEditHotelGUI(id, db);
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
