using BookingCom.model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookingCom.pages
{
    public partial class luuTru : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("luuTruDB");

        public static IMongoCollection<Room> collection = db.GetCollection<Room>("room");



        public luuTru()
        {
            InitializeComponent();
        }

        private void btn_addRoom_Click(object sender, EventArgs e)
        {
            // Declare variables
            string name = textBox_name.Text;
            string type = textBox_type.Text;
            string description = textBox_description.Text;
            List<string> amenities = textBox_amenities.Text.Split(',').ToList();
            int price = Convert.ToInt32(textBox_price.Text);

            // Register data
            Room room = new Room(name, type, description, amenities, price);

            collection.InsertOne(room);
        }

        public void readData()
        {
            var rooms = collection.Find(FilterDefinition<Room>.Empty).ToList();

            // Create a BindingList<Room> from the retrieved data
            BindingList<Room> roomList = new BindingList<Room>(rooms);

            // Set the data source for the dataGridView
            dataRoom.AutoGenerateColumns = true;
            dataRoom.DataSource = roomList;
        }


        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            readData();
        }
    }
}
