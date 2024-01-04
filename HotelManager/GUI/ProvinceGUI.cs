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
    public partial class ProvinceGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public ProvinceGUI()
        {
            InitializeComponent();             
            loadData();
        }
        public void loadData()
        {
            dtgProvince.Columns.Clear();
            try
            {
                var province = db.Provinces
                   .Select(p => new { p.Id, p.ProvinceName, p.Address })
                                .ToList();
                dtgProvince.DataSource = province;
                dtgProvince.Columns["Id"].Visible = false;
                int countC = dtgProvince.Columns.Count;
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
                {
                    Text = "Edit",
                    Name = "Edit",
                    UseColumnTextForButtonValue = true
                };
                dtgProvince.Columns.Insert(countC, btnEdit);

                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn
                {
                    Text = "Delete",
                    Name = "Delete",
                    UseColumnTextForButtonValue = true
                };
                dtgProvince.Columns.Insert(countC + 1, btnDelete);
            }
            catch (Exception)
            {
            }
            
        }

        private void cboProvince_SelectedValueChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void dtgProvince_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgProvince.Columns["Delete"].Index)
                {
                    int id = (int)dtgProvince.Rows[e.RowIndex].Cells["Id"].Value;
                    DialogResult dr = MessageBox.Show("Do you want to delete?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        try
                        {                        
                            Province province = db.Provinces.Find(id);
                            db.Provinces.Remove(province);
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
                if (e.ColumnIndex == dtgProvince.Columns["Edit"].Index)
                {
                    int id = (int)dtgProvince.Rows[e.RowIndex].Cells["Id"].Value;
                    AddEditProvinceGUI a = new AddEditProvinceGUI(id, db);
                    a.ShowDialog(this);
                    loadData();
                }
            }
            catch (Exception)
            {
            }            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditProvinceGUI a = new AddEditProvinceGUI(-1, db);
            a.ShowDialog(this);
            loadData();
        }
    }
}
