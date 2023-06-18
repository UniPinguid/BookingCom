using BookingCom.model;
using BookingCom.pages;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MongoDB.Driver.WriteConcern;

namespace WindowsFormsApp1.pages.luuTru
{
    public partial class luuTru_booking : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("luuTruDB");

        public static IMongoCollection<Stay> stayCollection = db.GetCollection<Stay>("stay");

        ObjectId bookingId = ObjectId.GenerateNewId();
        public string stayIdString;
        public List<string> roomList = new List<string>();
        public List<int> roomNumList = new List<int>();
        public DateTime checkinDate;
        public DateTime checkoutDate;
        public int noNights;
        public int noAdults;
        public int noChildren;
        public int total;

        public luuTru_booking()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            label_checkin.Text = checkinDate.ToString("dd/MM/yyyy");
            label_checkout.Text = checkoutDate.ToString("dd/MM/yyyy");
            label_adults_children.Text = noAdults.ToString() + " / " + noChildren.ToString();

            IMongoCollection<Stay> stayCollection = db.GetCollection<Stay>("stay");

            // Query for the stay with the specified stayId
            ObjectId stayId = ObjectId.Parse(stayIdString);

            var filter = Builders<Stay>.Filter.Eq("_id", stayId);
            var stay = stayCollection.Find(filter).FirstOrDefault();

            // Get room data
            IMongoCollection<Room> roomCollection = db.GetCollection<Room>("room");

            for (int i = 0; i < roomList.Count; i++)
            {
                var filterRoom = Builders<Room>.Filter.Eq(r => r.Id, ObjectId.Parse(roomList[i]));
                Room room = roomCollection.Find(filterRoom).FirstOrDefault();

                decimal total = room.Price * roomNumList[i];

                DataGridViewRow row = new DataGridViewRow();

                dgv_roomData.Columns.Add("RoomName", "Room Name");
                dgv_roomData.Columns.Add("RoomType", "Room Type");
                dgv_roomData.Columns.Add("Price", "Price");
                dgv_roomData.Columns.Add("Amount", "Amount");
                dgv_roomData.Columns.Add("Total", "Total");

                row.CreateCells(dgv_roomData);
                row.Cells[0].Value = room.Name;
                row.Cells[1].Value = room.Type;
                row.Cells[2].Value = room.Price;
                row.Cells[3].Value = roomNumList[i];
                row.Cells[4].Value = total;

                dgv_roomData.Rows.Add(row);
            }

            label_bookingId.Text = bookingId.ToString();

            label_name.Text = stay.Name.ToString();
            label_location.Text = stay.Location.ToString();
            label_address.Text = stay.Address.ToString();

            label_total.Text = "VND " + total.ToString();
        }

        private void paymentMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_alert_payment.Dispose();
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            Booking booking = new Booking
            {
                Id = bookingId, // Auto-generate new ObjectId
                UserId = ObjectId.GenerateNewId(),
                StayId = new ObjectId(stayIdString),
                RoomId = new List<ObjectId>(),
                RoomNum = new List<decimal>(),
                CheckInDate = checkinDate,
                CheckOutDate = checkoutDate,
                NumberOfNights = (int)(checkoutDate - checkinDate).TotalDays,
                PaymentMethod = paymentMethods.Text,
                PaymentStatus = "Pending",
                NumberOfAdults = noAdults,
                NumberOfChildren = noChildren,
                SpecialRequests = textBox_request.Text
            };


            for (int i = 0; i < roomList.Count; i++)
            {
                booking.RoomId.Add(ObjectId.Parse(roomList[i]));
                booking.RoomNum.Add(roomNumList[i]);
            }

            IMongoCollection<Booking> bookingCollection = db.GetCollection<Booking>("booking");
            bookingCollection.InsertOne(booking);

            MessageBox.Show("Đặt phòng thành công!");

            this.Close();
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
