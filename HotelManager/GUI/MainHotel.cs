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
    public partial class MainHotel : Form
    {
        private HotelManagerContext db = new HotelManagerContext();
        private string _username;
        public MainHotel(string username)
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
            var room = db.Rooms.ToList();
            LoadData();
        }
        private void LoadData()
        {
            dtgView.Columns.Clear();
            try
            {
                var room = (from r in db.Rooms select new { r.Id, r.RoomName, r.Status, r.RoomType,r.Price, r.HId }).Where(r => r.HId == (int)cboHotel.SelectedValue).ToList();
                dtgView.DataSource = room;
                dtgView.Columns["Id"].Visible = false;
                dtgView.Columns["HId"].Visible = false;

                int countC = dtgView.Columns.Count;
                DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn
                {
                    Text = "Book",
                    Name = "Book",
                    UseColumnTextForButtonValue = true
                };
                dtgView.Columns.Insert(countC, btnEdit);
                //this.dtgView.DefaultCellStyle.Font = new Font("Regular", 9);
            }
            catch (Exception)
            {
            }
            var user = db.Users.Where(u => u.Username.Equals(_username)).SingleOrDefault();
            decimal money = Convert.ToDecimal(user.Money);
            if (user != null)
            {
                lblMoney.Text = Math.Round(money, 0).ToString() + " VNĐ";
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
            LoadData();
        }

        private void dtgView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dtgView.Columns["Book"].Index)
                {
                    int id = (int)dtgView.Rows[e.RowIndex].Cells["Id"].Value;
                    Room room1 = db.Rooms.Find(id);
                    if (!room1.Status.Equals("Available"))
                    {
                        MessageBox.Show("This room have booked by another user\n Choose another room");
                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show($"Do you want to book this room with price {Math.Round(decimal.Parse(room1.Price.ToString()),0)} VNĐ/Day?", "Confirm",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dr == DialogResult.Yes)
                        {
                            try
                            {
                                User user = db.Users.Where(u => u.Username == _username).Single();
                                Room room = db.Rooms.Find(id);

                                var checkOrder = db.Orders.Where(o => o.UId == user.Id &&
                                o.Status.Equals("InUse")).SingleOrDefault();

                                if (checkOrder == null)
                                {
                                    int orderId = db.Orders.Select(o => o.Id).Max();
                                    if (room.RoomType.Equals("VIP"))
                                    {
                                        if (user.Money < room.Price)
                                        {
                                            MessageBox.Show("You not enough money to booking, please recharge!");
                                        }
                                        else
                                        {
                                            user.Money = user.Money - room.Price;
                                            MessageBox.Show("Book this room success.");
                                            room.Status = "Booked";
                                            OrderRoom or = new OrderRoom
                                            {
                                                OId = orderId + 1,
                                                RId = room.Id
                                            };
                                            Order order = new Order
                                            {
                                                UId = user.Id,
                                                Total = room.Price,
                                                DateBook = DateTime.Now,
                                                DateCheckout = DateTime.Now,
                                                Status = "InUse"
                                            };
                                            db.Rooms.Update(room);
                                            db.Users.Update(user);
                                            db.Add(or);
                                            db.Orders.Add(order);
                                            db.SaveChanges();
                                            LoadData();
                                        }
                                    }
                                    else
                                    {
                                        if (user.Money < room.Price)
                                        {
                                            MessageBox.Show("You not enough money to book this room, please recharge!");
                                        }
                                        else
                                        {
                                            user.Money = user.Money - room.Price;
                                            MessageBox.Show("Book this room success.");
                                            room.Status = "Booked";
                                            OrderRoom or = new OrderRoom
                                            {
                                                OId = orderId + 1,
                                                RId = room.Id
                                            };
                                            Order order = new Order
                                            {
                                                UId = user.Id,
                                                Total = room.Price,
                                                DateBook = DateTime.Now,
                                                DateCheckout = DateTime.Now,
                                                Status = "InUse"
                                            };
                                            db.Rooms.Update(room);
                                            db.Users.Update(user);
                                            db.Orders.Add(order);
                                            db.Add(or);
                                            db.SaveChanges();
                                            LoadData();
                                        }
                                    }
                                    LoadData();
                                }
                                else
                                {
                                    int orderId = checkOrder.Id;
                                    if (room.RoomType.Equals("VIP"))
                                    {
                                        if (user.Money < room.Price)
                                        {
                                            MessageBox.Show("You not enough money to booking, please recharge!");
                                        }
                                        else
                                        {
                                            user.Money = user.Money - room.Price;
                                            MessageBox.Show("Book this room success.");
                                            room.Status = "Booked";
                                            OrderRoom or = new OrderRoom
                                            {
                                                OId = orderId,
                                                RId = room.Id
                                            };
                                            checkOrder.Total += room.Price;
                                            checkOrder.DateCheckout = DateTime.Now;
                                            db.Rooms.Update(room);
                                            db.Users.Update(user);
                                            db.Add(or);
                                            db.Orders.Update(checkOrder);
                                            db.SaveChanges();
                                            LoadData();
                                        }
                                    }
                                    else
                                    {
                                        if (user.Money < room.Price)
                                        {
                                            MessageBox.Show("You not enough money to book this room, please recharge!");
                                        }
                                        else
                                        {
                                            user.Money = user.Money - room.Price;
                                            MessageBox.Show("Book this room success.");
                                            room.Status = "Booked";
                                            OrderRoom or = new OrderRoom
                                            {
                                                OId = orderId,
                                                RId = room.Id
                                            };
                                            checkOrder.Total += room.Price;
                                            checkOrder.DateCheckout = DateTime.Now;
                                            db.Rooms.Update(room);
                                            db.Users.Update(user);
                                            db.Orders.Update(checkOrder);
                                            db.Add(or);
                                            db.SaveChanges();
                                            LoadData();
                                        }
                                    }
                                    LoadData();
                                }                               
                            }
                            catch
                            {
                                MessageBox.Show("Failled!");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
