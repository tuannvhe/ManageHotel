using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManager.GUI
{
    public partial class AllOrderGUI : Form
    {
        HotelManagerContext db = new HotelManagerContext();
        public AllOrderGUI()
        {
            InitializeComponent();
            loadData();
            lblTotal.Visible = false;
        }
        private void loadData()
        {
            dtgBooking.Columns.Clear();
            try
            {         /**/     
                var order = from o in db.Orders select new {o.Id,
                    o.UIdNavigation.Username,
                    o.DateBook, 
                    o.DateCheckout,
                    o.Total,
                    o.Status
                    };
                dtgBooking.DataSource = order.ToList();
                dtgBooking.Columns["Id"].Visible = false;
                lblTotals.Text = $"Total: {Math.Round(calTotal(), 0)} VNĐ";
            }
            catch (Exception)
            {
            }
        }   
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFrom.Text.Length == 0 || txtTo.Text.Length == 0)
                {
                    loadData();
                }
                else
                {
                    var order = from o in db.Orders
                                where o.DateCheckout <= DateTime.Parse(txtTo.Text)
                                && o.DateCheckout >= DateTime.Parse(txtFrom.Text)
                                select new
                                {
                                    o.Id,
                                    o.UIdNavigation.Username,
                                    o.DateBook,
                                    o.DateCheckout,
                                    o.Total,
                                    o.Status
                                };                    
                    dtgBooking.DataSource = order.ToList();
                    decimal total = 0;
                    for (int i = 0; i < dtgBooking.RowCount; i++)
                    {
                        total += decimal.Parse(dtgBooking.Rows[i].Cells[1].Value.ToString());
                    }
                    int money = dtgBooking.Rows.Count;
                    for (int i = 0; i < money - 1; i++)
                    {
                        total += decimal.Parse(dtgBooking.Rows[i].Cells["Total"].Value.ToString());
                    }
                    lblTotals.Text = $"Total: {Math.Round(total, 0)} VNĐ";
                    dtgBooking.Columns["Id"].Visible = false;

                }                               
            }
            catch (Exception)
            {
            }          
        }
        private decimal calTotal()
        {
            int money = dtgBooking.Rows.Count;
            decimal total = 0;
            for (int i = 0; i < money - 1; i++)
            {
                total += decimal.Parse(dtgBooking.Rows[i].Cells["Total"].Value.ToString());
            }
            return total;
        }
        private void dtgBooking_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = (int)dtgBooking.Rows[e.RowIndex].Cells["Id"].Value;
                var orderDetails = from o1 in db.OrderServices
                                   join o2 in db.Services
                                   on o1.SId equals o2.Id
                                   where o1.OId == id
                                   select new
                                   {
                                       o1.OId,
                                       o2.ServiceName,
                                       o1.Quantity
                                   };
                dtgDetails.DataSource = orderDetails.ToList(); 
                dtgDetails.Columns["OId"].Visible = false;
            }
            catch (Exception)
            {
            }
        }
    }
}
