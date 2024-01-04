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
    public partial class MainService : Form
    {
        private HotelManagerContext db = new HotelManagerContext();
        private string _username;
        private decimal total = 0;
        public MainService(string username)
        {
            InitializeComponent();
            _username = username;
            var province = (from p in db.Provinces select new { p.Id, p.ProvinceName }).ToList();
            cboProvince.DataSource = province;
            cboProvince.DisplayMember = "ProvinceName";
            cboProvince.ValueMember = "Id";

            var hotel = (from h in db.Hotels select new { h.Id, h.HotelName, h.PId }).Where(h => h.PId == (int)cboProvince.SelectedValue).ToList();
            cboHotel.DataSource = hotel;
            cboHotel.DisplayMember = "HotelName";
            cboHotel.ValueMember = "Id";
            var user = db.Users.Where(u => u.Username == _username).Single();
            var services = (from s in db.Services select new { s.Id, s.Type, s.HId }).ToList();            
            loadData();

            var order = db.Orders.Where(o => o.UId == user.Id && o.Status == "InUse").SingleOrDefault();

            if (order != null)
            {
                var orderRoom = db.OrderRooms.Where(o => o.OId == order.Id).ToList();

                var roomBooked = from r1 in db.Rooms
                                 join r2 in db.OrderRooms.Where(o => o.OId == order.Id)
                                 on r1.Id equals r2.RId
                                 join r3 in db.Hotels
                                 on r1.HId equals r3.Id
                                 select new
                                 {
                                     r1.Id,
                                     r1.RoomName,
                                     r1.HId
                                 };
                var data = roomBooked.Where(r => r.HId == (int)cboHotel.SelectedValue).ToList();

                cboRoom.DataSource = data;
                cboRoom.DisplayMember = "RoomName";
                cboRoom.ValueMember = "Id";
            }
            else
            {
                cboRoom.Items.Clear();
            }

        }      
        private void loadData()
        {
            dtgBill.Columns.Clear();
            dtgView.Columns.Clear();
            
            try
            {
                var service = (from s in db.Services select new { s.Id, s.ServiceName, s.Price, s.Type, s.Image, s.HId }).Where(s => s.HId == (int)cboHotel.SelectedValue).ToList();
                dtgView.DataSource = service;
                dtgView.Columns["Id"].Visible = false;
                dtgView.Columns["HId"].Visible = false;
                dtgView.Columns["Image"].Visible = false;
                string imageLocation = "";
                imageLocation = dtgView.Rows[0].Cells["Image"].FormattedValue.ToString();
                pictureBox.ImageLocation = imageLocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                int countC = dtgView.Columns.Count;
                DataGridViewButtonColumn btnAdd = new DataGridViewButtonColumn
                {
                    Text = "Add",
                    Name = "Add",
                    UseColumnTextForButtonValue = true
                };
                dtgView.Columns.Insert(countC, btnAdd);            

                User user = db.Users.Where(u => u.Username == _username).Single();
                int orderId = db.Orders.Select(o => o.Id).Max();                
                var bill = from b1 in db.OrderServices
                           join b2 in db.Services
                           on b1.SId equals b2.Id
                           where b1.OId == orderId
                           select new
                           {
                               b1.Id,
                               b1.Quantity,
                               b2.ServiceName,
                               b2.Price,
                               b1.SId
                           };
                dtgBill.DataSource = bill.ToList();
                int count = dtgBill.Columns.Count;
                DataGridViewButtonColumn btnRemove = new DataGridViewButtonColumn
                {
                    Text = "Remove",
                    Name = "Remove",
                    UseColumnTextForButtonValue = true
                };
                dtgBill.Columns.Insert(count, btnRemove);
                dtgBill.Columns["SId"].Visible = false;
                dtgBill.Columns["Id"].Visible = false;
                dtgBill.Columns["Price"].Visible = false;
                lblMoney.Text = $"{Math.Round(decimal.Parse(user.Money.ToString()), 0)} VNĐ";
                lblTotal.Text = $"{Math.Round(total, 0)} VNĐ";

               
                
                lblMoney.Text = $"{Math.Round(decimal.Parse(user.Money.ToString()), 0)} VNĐ";
                lblTotal.Text = $"{Math.Round(GetTotal(), 0)} VNĐ";
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
                cboHotel.ValueMember = "Id";
            }
            catch (Exception)
            {
            }
        }
        public decimal GetTotal()
        {
            User user1 = db.Users.Where(u => u.Username == _username).Single();
            var orderId = db.Orders.Where(o => o.UId == user1.Id).Single();
            var service = db.Services.ToList();
            decimal? total = (from cartItems in db.OrderServices
                              where cartItems.OId == orderId.Id
                              select (int?)cartItems.Quantity * cartItems.Service.Price).Sum();
            return total ?? 0;
        }
        private void cboHotel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                User user = db.Users.Where(u => u.Username == _username).Single();
                var order = db.Orders.Where(o => o.UId == user.Id && o.Status == "InUse").SingleOrDefault();
                
                if (order != null)
                {
                    var orderRoom = db.OrderRooms.Where(o => o.OId == order.Id).ToList();

                    var roomBooked = from r1 in db.Rooms
                                     join r2 in db.OrderRooms.Where(o => o.OId == order.Id)
                                     on r1.Id equals r2.RId
                                     join r3 in db.Hotels
                                     on r1.HId equals r3.Id
                                     select new
                                     {
                                         r1.Id,
                                         r1.RoomName,
                                         r1.HId
                                     };
                    var data = roomBooked.Where(r => r.HId == (int)cboHotel.SelectedValue).ToList();

                    cboRoom.DataSource = data;
                    cboRoom.DisplayMember = "RoomName";
                    cboRoom.ValueMember = "Id";
                }
                else
                {
                    cboRoom.ValueMember = "";
                    cboRoom.DisplayMember = "";
                }
            }
            catch (Exception)
            {
            }
            loadData();
        }
        
        private void dtgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            try
            {
                string imageLocation = "";
                imageLocation = dtgView.Rows[e.RowIndex].Cells["Image"].FormattedValue.ToString();
                pictureBox.ImageLocation = imageLocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                if (e.ColumnIndex == dtgView.Columns["Add"].Index)
                {
                    int id = (int)dtgView.Rows[e.RowIndex].Cells["Id"].Value;
                    var item = db.Services.Where(s => s.Id == id).Single();
                    
                    decimal totalItem = nudQuantity.Value * (decimal)item.Price;
                    int orderId = db.Orders.Select(o => o.Id).Max();                        

                    try
                    {                          
                         User user = db.Users.Where(u => u.Username == _username).Single();

                         int sid = int.Parse(dtgView.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                         var service = db.Services.Where(s => s.Id == id && s.HId == (int)cboHotel.SelectedValue).Single();
                         var order = db.OrderServices.Where(o => o.OId == orderId 
                         && o.SId == id)
                         .SingleOrDefault();

                         if (order!=null)
                         {
                            order.Quantity = order.Quantity + (int)nudQuantity.Value;
                            total += (int)nudQuantity.Value * decimal.Parse(service.Price.ToString());

                            db.OrderServices.Update(order);
             
                            db.SaveChanges();
                            nudQuantity.Value = 1;
                            loadData();
                         }
                         else
                         {
                            OrderService orderService = new OrderService
                            {
                                OId = orderId,
                                SId = sid,
                                Quantity = (int)nudQuantity.Value
                            };
                            db.OrderServices.Add(orderService);
                            db.SaveChanges();
                            nudQuantity.Value = 1;
                            total += (int)nudQuantity.Value * decimal.Parse(service.Price.ToString());
                            loadData();
                         }

                    }
                    catch (Exception )
                    {
                    }                    
                }
            }
            catch (Exception)
            {
            }
        }       

        private void btnClear_Click(object sender, EventArgs e)
        {
            int orderId = db.Orders.Select(o => o.Id).Max();
            var orderServices = db.OrderServices.Where(o => o.OId == orderId);

            db.RemoveRange(orderServices);
            db.SaveChanges();
            loadData();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (dtgBill.Rows.Count == 0)
            {
                MessageBox.Show("Your bill is empty!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cboRoom.Items.Count==0)
            {
                MessageBox.Show("You not have room!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dr = MessageBox.Show("Do you want to order all of this items?", "Confirm",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    User user = db.Users.Where(u => u.Username == _username).Single();
                    if (user.Money < total)
                    {
                        MessageBox.Show("You are not enough money to order!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        try
                        {
                            Order order = new Order
                            {
                                UId = user.Id,
                                Total = total,
                                DateBook = DateTime.Now,
                                DateCheckout = DateTime.Now,
                                Status = "Order"
                            };

                            user.Money = user.Money - total;
                            db.Orders.Add(order);
                            db.Users.Update(user);
                            db.SaveChanges();
                            MessageBox.Show("Order Success!");

                            loadData();                  
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }                   
                }
            }          
        }

        private void dtgBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            try
            {
                string imageLocation = "";
                int sid = (int)dtgBill.Rows[e.RowIndex].Cells["SId"].Value;
                var selectService = db.Services.Where(s => s.Id == sid).Single();
                imageLocation = selectService.Image.ToString();
                pictureBox.ImageLocation = imageLocation;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                if (e.ColumnIndex == dtgBill.Columns["Remove"].Index)
                {
                    int id = (int)dtgBill.Rows[e.RowIndex].Cells["Id"].Value;

                    User user = db.Users.Where(u => u.Username == _username).Single();
                    var order = db.OrderServices.Find(id);

                    var service = db.Services.Where(s => s.Id == order.SId).Single();
                    if (order.Quantity == 1)
                    {
                        db.OrderServices.Remove(order);
                        db.SaveChanges();
                    }
                    else
                    {
                        order.Quantity -= 1;
                        db.OrderServices.Update(order);
                        db.SaveChanges();
                    }
                    loadData();
                }
            }
            catch (Exception)
            {
            }           
        }
    }
}
