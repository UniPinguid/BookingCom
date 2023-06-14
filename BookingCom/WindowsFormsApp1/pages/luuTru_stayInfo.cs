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
using System.Xml;
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

        public luuTru_stayInfo()
        {
            InitializeComponent();
            this.Size = new Size(1180, 650);
        }

        public luuTru_stayInfo(ObjectId stayId)
        {
            InitializeComponent();
            this.Size = new Size(1180, 650);

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
            }
            else
            {
                // Handle the case when no stay is found
                label_name.Text = "";
                label_location.Text = "";
                label_description.Text = "";
                label_score.Text = "";
            }

            positioning();
            DisplayRoomData(stayId);
        }

        void positioning()
        {
            int height = label_description.Height;

            label_header_availability.Location = new Point(label_header_availability.Location.X, label_header_availability.Location.Y + height);
            groupBoxesPanel.Location = new Point(groupBoxesPanel.Location.X, groupBoxesPanel.Location.Y + height);
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
                label_amenities.BringToFront();

                Label label_sleeps = new Label();
                label_sleeps.Text = "Sleeps: " + room.Sleeps.ToString();
                luuTru.cloneLabel("label_sleeps", label_sleeps, groupBox_room);

                Label label_roomPrice = new Label();
                label_roomPrice.Text = "VND " + room.Price.ToString();
                luuTru.cloneLabel("label_roomPrice", label_roomPrice, groupBox_room);

                Button buttonDec = new Button();
                buttonDec.Text = btn_dec.ToString();
                luuTru.cloneButton("btn_dec", buttonDec, groupBox_room);
                // buttonDec.Click += buttonDec_click;

                Button buttonInc = new Button();
                buttonInc.Text = btn_inc.ToString();
                luuTru.cloneButton("btn_inc", buttonInc, groupBox_room);
                // buttonDec.Click += buttonDec_click;

                TextBox textBoxAmount = new TextBox();
                textBoxAmount.Text = "0";
                luuTru.cloneTextBox("textBox_amount", textBoxAmount, groupBox_room);

                // Add the labels to the GroupBox
                groupBox.Controls.Add(label_roomName);
                groupBox.Controls.Add(label_roomType);
                groupBox.Controls.Add(label_sleeps);
                groupBox.Controls.Add(label_amenities);
                groupBox.Controls.Add(label_roomPrice);

                // Add buttons to the GroupBox
                groupBox.Controls.Add(buttonDec);
                groupBox.Controls.Add(buttonInc);
                groupBox.Controls.Add(textBoxAmount);

                // Add the cloned GroupBox to the panel or container
                groupBoxesPanel.Controls.Add(groupBox);

                // Update the Y-coordinate position for the next GroupBox
                groupBoxY += 150;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
