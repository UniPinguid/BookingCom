using BookingCom.model;
using BookingCom.pages;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WindowsFormsApp1.pages.luuTru;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using GroupBox = System.Windows.Forms.GroupBox;

namespace BookingCom.pages
{
    public partial class luuTru_stayInfo : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("luuTruDB");

        public static IMongoCollection<Stay> stayCollection = db.GetCollection<Stay>("stay");

        public static string stayIdString = "";

        public DateTime checkinDate;
        public DateTime checkoutDate;
        public int noNights;
        public int noAdults;
        public int noChildren;

        int total = 0;

        public luuTru_stayInfo()
        {
            InitializeComponent();
            this.Size = new Size(1140, 680);
        }

        public luuTru_stayInfo(ObjectId stayId)
        {
            InitializeComponent();
            this.Size = new Size(1140, 680);

            IMongoCollection<Stay> stayCollection = db.GetCollection<Stay>("stay");

            // Query for the stay with the specified stayId
            var filter = Builders<Stay>.Filter.Eq("_id", stayId);
            var stay = stayCollection.Find(filter).FirstOrDefault();

            if (stay != null)
            {
                // Populate the labels with the stay data
                label_name.Text = stay.Name;
                label_location.Text = stay.Location;
                label_description.Text = stay.Description;
                label_score.Text = stay.Score.ToString();
                label_address.Text = stay.Address;
            }
            else
            {
                // Handle the case when no stay is found
                label_name.Text = "";
                label_location.Text = "";
                label_description.Text = "";
                label_score.Text = "";
            }

            stayIdString = stay.Id.ToString();

            positioning();
            DisplayRoomData(stayId);
        }

        void positioning()
        {
            int height = label_description.Height;

            label_header_availability.Location = new Point(label_header_availability.Location.X, label_header_availability.Location.Y + height);
            groupBoxesPanel.Location = new Point(groupBoxesPanel.Location.X, groupBoxesPanel.Location.Y + height);

            btn_reserve.Location = new Point(btn_reserve.Location.X, btn_reserve.Location.Y + height);

            label_header_review.Location = new Point(label_header_review.Location.X, label_header_review.Location.Y + height);
        }

        public void DisplayRoomData(ObjectId stayId)
        {
            // Assuming you have a MongoDB collection for "room" models
            IMongoCollection<Room> roomCollection = db.GetCollection<Room>("room");

            // Create a filter to match rooms with the specified stay ID
            var filter = Builders<Room>.Filter.Eq("stayId", stayId);

            // Retrieve the room data for the specific stay
            var roomData = roomCollection.Find(filter).ToList();

            // Clear existing GroupBoxes before populating new ones
            groupBoxesPanel.Controls.Clear();

            int groupBoxY = groupBox_room.Location.Y;

            // Display the room data on the page
            foreach (var room in roomData)
            {
                GroupBox groupBox = new GroupBox();

                // Set the properties of the GroupBox
                groupBox.Size = groupBox_room.Size;
                groupBox.Location = new Point(groupBox_room.Location.X, groupBoxY);
                groupBox.Name = room.Id.ToString();

                Label label_roomName = new Label();
                label_roomName.Text = room.Name;
                luuTru.cloneLabel("label_roomName", label_roomName, groupBox_room);

                Label label_roomType = new Label();
                label_roomType.Text = room.Type;
                luuTru.cloneLabel("label_roomType", label_roomType, groupBox_room);

                Label label_amenities = new Label();
                string amenitiesText = string.Join(", ", room.Amenities);
                label_amenities.Text = amenitiesText;
                luuTru.cloneLabel("label_amenities", label_amenities, groupBox_room);

                Label label_sleeps = new Label();
                label_sleeps.Text = "Sleeps: " + room.Sleeps.ToString();
                luuTru.cloneLabel("label_sleeps", label_sleeps, groupBox_room);

                Label label_availability = new Label();
                label_availability.Text = room.Availability.ToString() + " left";
                luuTru.cloneLabel("label_availability", label_availability, groupBox_room);

                Label label_roomPrice = new Label();
                label_roomPrice.Text = "VND " + room.Price.ToString();
                luuTru.cloneLabel("label_roomPrice", label_roomPrice, groupBox_room);

                Button buttonDec = new Button();
                buttonDec.Text = btn_dec.ToString();
                buttonDec.Tag = room.Id.ToString();
                luuTru.cloneButton("btn_dec", buttonDec, groupBox_room);

                Button buttonInc = new Button();
                buttonInc.Text = btn_inc.ToString();
                buttonInc.Tag = room.Id.ToString();
                luuTru.cloneButton("btn_inc", buttonInc, groupBox_room);

                TextBox textBoxAmount = new TextBox();
                textBoxAmount.Text = "0";
                textBoxAmount.Name = room.Id.ToString();
                luuTru.cloneTextBox("textBox_amount", textBoxAmount, groupBox_room);

                // Add the labels to the GroupBox
                groupBox.Controls.Add(label_roomName);
                groupBox.Controls.Add(label_roomType);
                groupBox.Controls.Add(label_sleeps);

                if (room.Availability < 10)
                    groupBox.Controls.Add(label_availability);

                groupBox.Controls.Add(label_amenities);
                groupBox.Controls.Add(label_roomPrice);

                // Add buttons to the GroupBox
                buttonDec.Click += btn_dec_Click;
                groupBox.Controls.Add(buttonDec);

                buttonInc.Click += btn_inc_Click;
                groupBox.Controls.Add(buttonInc);

                groupBox.Controls.Add(textBoxAmount);

                // Add the cloned GroupBox to the panel or container
                groupBoxesPanel.Controls.Add(groupBox);

                // Update the Y-coordinate position for the next GroupBox
                groupBoxY += 150;
            }
        }

        private void btn_inc_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string roomId = clickedButton.Tag.ToString();

            GroupBox groupBox = groupBoxesPanel.Controls.Find(roomId, true).FirstOrDefault() as GroupBox;
            TextBox textBoxAmount = groupBox.Controls.Find(roomId, true).FirstOrDefault() as TextBox;

            IMongoCollection<Room> roomCollection = db.GetCollection<Room>("room");

            // Build the filter condition
            var filter = Builders<Room>.Filter.Eq(r => r.Id, ObjectId.Parse(roomId));

            // Execute the query and retrieve the matching room
            Room room = roomCollection.Find(filter).FirstOrDefault();

            int amount = Convert.ToInt32(textBoxAmount.Text);
            if (amount < room.Availability)
            {
                amount++;
                textBoxAmount.Text = amount.ToString();
                updateTotalPrice(groupBox);
            }
        }

        private void btn_dec_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string roomId = clickedButton.Tag.ToString();

            GroupBox groupBox = groupBoxesPanel.Controls.Find(roomId, true).FirstOrDefault() as GroupBox;
            TextBox textBoxAmount = groupBox.Controls.Find(roomId, true).FirstOrDefault() as TextBox;

            IMongoCollection<Room> roomCollection = db.GetCollection<Room>("room");

            // Build the filter condition
            var filter = Builders<Room>.Filter.Eq(r => r.Id, ObjectId.Parse(roomId));

            // Execute the query and retrieve the matching room
            Room room = roomCollection.Find(filter).FirstOrDefault();

            int amount = Convert.ToInt32(textBoxAmount.Text);

            if (amount > 0)
            {
                amount--;
                textBoxAmount.Text = amount.ToString();
                updateTotalPrice(groupBox);
            }
        }

        private void updateTotalPrice(GroupBox groupBox)
        {
            int room_total = 0;

            foreach (Control control in groupBoxesPanel.Controls)
            {
                if (control is GroupBox group)
                {
                    foreach (Control childControl in group.Controls)
                    {
                        if (childControl is TextBox textBox)
                        {
                            string roomId = childControl.Name.ToString();

                            IMongoCollection<Room> roomCollection = db.GetCollection<Room>("room");

                            // Build the filter condition
                            var filter = Builders<Room>.Filter.Eq(r => r.Id, ObjectId.Parse(roomId));

                            // Execute the query and retrieve the matching room
                            Room room = roomCollection.Find(filter).FirstOrDefault();

                            room_total += (int)room.Price * Convert.ToInt32(childControl.Text);
                        }
                    }
                }
            }

            total = room_total;
            label_total.Text = "VND " + total.ToString();
        }


        private void btn_reserver_MouseHover(object sender, EventArgs e)
        {
            btn_reserve.BackColor = Color.FromArgb(255, 11, 87, 163);

        }

        private void btn_reserver_MouseLeave(object sender, EventArgs e)
        {
            btn_reserve.BackColor = Color.FromArgb(255, 0, 102, 204);
        }

        private void btn_reserve_click(object sender, EventArgs e)
        {
            List<string> roomIdList = new List<string>();
            List<int> roomNumList = new List<int>();

            // Get list of booked rooms
            foreach (Control control in groupBoxesPanel.Controls)
            {
                if (control is GroupBox group)
                {
                    foreach (Control childControl in group.Controls)
                    {
                        if (childControl is TextBox textBox)
                        {
                            if (Convert.ToInt32(childControl.Text) > 0)
                            {
                                string roomId = childControl.Name.ToString();
                                roomIdList.Add(roomId);
                                roomNumList.Add(Convert.ToInt16(childControl.Text));
                            }
                        }
                    }
                }
            }

            if (roomIdList.Count > 0)
            {
                luuTru_booking bookingPage = new luuTru_booking();

                bookingPage.roomList = roomIdList;
                bookingPage.roomNumList = roomNumList;
                bookingPage.stayIdString = stayIdString;
                bookingPage.checkinDate = checkinDate;
                bookingPage.checkoutDate = checkoutDate;
                bookingPage.noNights = noNights;
                bookingPage.noAdults = noAdults;
                bookingPage.noChildren = noChildren;
                bookingPage.total = total;
                bookingPage.Show();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn phòng và số lượng phòng");
            }
        }
    }
}
