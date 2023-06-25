using BookingCom.model;
using MongoDB.Bson;
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
using System.Xml;
using BookingCom.pages;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using GroupBox = System.Windows.Forms.GroupBox;
using WindowsFormsApp1.pages;

namespace BookingCom.pages
{
    public partial class datXe : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("datXeDB");
        public static IMongoCollection<Car> collection = db.GetCollection<Car>("Car");

        //public string PickUpLocation { get; set; }
        //public string DropOffLocation { get; set; }
        //public string PickUpTime { get; set; }
        //public string DropOffTime { get; set; }
        //public string PickUpDate { get; set; }
        //public string DropOffDate { get; set; }
        //public int Price { get; set; }

        public datXe()
        {
            InitializeComponent();
            dateTimePicker_dropoff.Value = DateTime.Now.AddDays(3); 
            TimKiem_Xe();
        }

        private void checkBox_DropOff_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DropOff.Checked)
            {
                label_DropOffLocation.Visible = true;
                txtbox_DropOffLocation.Visible = true;
            }
            else
            {
                label_DropOffLocation.Visible = false;
                txtbox_DropOffLocation.Visible = false;
            }
        }

        public static void cloneLabel(string oldLabelName, Label newLabel, GroupBox groupBox)
        {
            Label oldLabel = groupBox.Controls.Find(oldLabelName, true).FirstOrDefault() as Label;

            newLabel.Font = oldLabel.Font;
            newLabel.ForeColor = oldLabel.ForeColor;
            newLabel.BackColor = oldLabel.BackColor;
            newLabel.Location = oldLabel.Location;

            bool autoSize = newLabel.AutoSize;
            newLabel.AutoSize = autoSize;

            newLabel.Width = oldLabel.Width;
            newLabel.Height = oldLabel.Height;

            ContentAlignment textAlign = oldLabel.TextAlign;
            newLabel.TextAlign = textAlign;
            newLabel.Visible = oldLabel.Visible;
        }
        public static void cloneButton(string oldButtonName, Button newButton, GroupBox groupBox)
        {
            Button oldButton = groupBox.Controls.Find(oldButtonName, true).FirstOrDefault() as Button;

            newButton.Text = oldButton.Text;

            newButton.FlatAppearance.BorderColor = oldButton.FlatAppearance.BorderColor;
            newButton.FlatAppearance.BorderSize = oldButton.FlatAppearance.BorderSize;
            newButton.FlatAppearance.MouseDownBackColor = oldButton.FlatAppearance.MouseDownBackColor;
            newButton.FlatAppearance.MouseOverBackColor = oldButton.FlatAppearance.MouseOverBackColor;

            newButton.Font = oldButton.Font;
            newButton.ForeColor = oldButton.ForeColor;
            newButton.BackColor = oldButton.BackColor;
            newButton.Location = oldButton.Location;

            bool autoSize = oldButton.AutoSize;
            newButton.AutoSize = autoSize;

            newButton.Width = oldButton.Width;
            newButton.Height = oldButton.Height;

            ContentAlignment textAlign = oldButton.TextAlign;
            newButton.TextAlign = textAlign;
        }

        public void TimKiem_Xe()
        {
            string pickup_location = txtbox_PickUpLocation.Text;
            string dropoff_location = txtbox_DropOffLocation.Text;
            string pickup_date = dateTimePicker_pickup.Value.ToString();
            string dropoff_date = dateTimePicker_dropoff.Value.ToString();
            string pickup_time = comboBox_pickuptime.Text;
            string dropoff_time = comboBox_dropofftime.Text;

            var filter = Builders<Car>.Filter.And(
                Builders<Car>.Filter.Eq(x => x.CarLocation,pickup_location),
                Builders<Car>.Filter.Eq(x => x.Available, true));
            var carlist = collection.Find(filter).ToList();

            panel_carlist.Controls.Clear();

            // Set the initial Y-coordinate position
            int groupBoxY = groupBox_model.Location.Y;

            // Create a GroupBox for each "Stay" record
            foreach (var car in carlist)
            {
                GroupBox groupBox = new GroupBox();

                // Set the properties of the GroupBox
                groupBox.Size = groupBox_model.Size;
                groupBox.Location = new Point(groupBox_model.Location.X, groupBoxY);

                // Create and set the properties of the labels inside the GroupBox
                Label nameLabel = new Label();
                nameLabel.Text = car.CarBrand;
                cloneLabel("label_CarName", nameLabel, groupBox_model);

                Label seatLabel = new Label();
                seatLabel.Text = car.NumberOfSeats.ToString() + " seats";
                cloneLabel("label_Seats", seatLabel, groupBox_model);

                Label bagLabel = new Label();
                bagLabel.Text = car.Luggages;
                cloneLabel("label_Luggages", bagLabel, groupBox_model);

                Label transLabel = new Label();
                transLabel.Text = "Transmission: " + car.Transmission;
                cloneLabel("label_Transmission", transLabel, groupBox_model);

                Label milesLabel = new Label();
                milesLabel.Text = car.NumberOfMiles.ToString() + " Miles";
                cloneLabel("label_Miles", milesLabel, groupBox_model);

                Label rentalLabel = new Label();
                rentalLabel.Text = car.CarRentalProvider.Name;
                cloneLabel("label_RentalProvider", rentalLabel, groupBox_model);

                Label locationLabel = new Label();
                locationLabel.Text = car.CarLocation;
                cloneLabel("label_CarLocation", locationLabel, groupBox_model);

                Label IDLabel = new Label();
                IDLabel.Text = car.Id.ToString();
                cloneLabel("label_IDCar",IDLabel, groupBox_model);

                Label scoreLabel = new Label();
                scoreLabel.Text = car.Reviews.Score.ToString();
                cloneLabel("label_Score", scoreLabel, groupBox_model);
                scoreLabel.Tag = car.Id.ToString();
                scoreLabel.MouseHover += label_Score_MouseHover;

                TimeSpan duration = dateTimePicker_dropoff.Value.Subtract(dateTimePicker_pickup.Value);
                Label priceLabel = new Label();
                priceLabel.Text = "đ "+(car.Price * (int)duration.TotalDays).ToString();
                cloneLabel("label_PriceValue",priceLabel, groupBox_model);

                // Add the labels to the GroupBox
                groupBox.Controls.Add(nameLabel);
                groupBox.Controls.Add(seatLabel);
                groupBox.Controls.Add(bagLabel);
                groupBox.Controls.Add(scoreLabel);
                groupBox.Controls.Add(transLabel);
                groupBox.Controls.Add(milesLabel);
                groupBox.Controls.Add(rentalLabel);
                groupBox.Controls.Add(locationLabel);
                groupBox.Controls.Add(IDLabel);
                groupBox.Controls.Add(priceLabel);

                // Clone the "btn_info" button and add it to the GroupBox
                Button viewButton = new Button();
                cloneButton("button_ViewDeal", viewButton, groupBox_model);
                viewButton.Tag = car.Id.ToString();
                viewButton.Click += button_ViewDeal_Click;
                groupBox.Controls.Add(viewButton);

                // Add the cloned GroupBox to the panel or container
                panel_carlist.Controls.Add(groupBox);

                // Update the Y-coordinate position for the next GroupBox
                groupBoxY += 200;
            }
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            TimKiem_Xe();
        }

        private void label_Score_MouseHover(object sender, EventArgs e)
        {
            Label HoverLabel = (Label)sender;
            ObjectId carID = ObjectId.Parse(HoverLabel.Tag.ToString());
            ToolTip toolTip = new ToolTip();

            var filter = Builders<Car>.Filter.Eq(c => c.Id, carID);
            var projection = Builders<Car>.Projection.Include(c => c.Reviews);

            var car = collection.Find(filter)
                    .Project<Car>(projection)
                    .FirstOrDefault();
            if (car != null)
            {
                toolTip.SetToolTip(HoverLabel, "Car Cleanliness: " + car.Reviews.CarCleanliness + "\n" +
                    "Pick Up Speed: " + car.Reviews.PickUpSpeed + "\n" +
                    "Drop Off Speed: " + car.Reviews.DropOffSpeed + "\n" +
                    "Car Condition: " + car.Reviews.CarCondition + "\n" +
                    "Helpfulness: " + car.Reviews.Helpfulness + "\n" +
                    "Easy To Find: " + car.Reviews.EasyToFind + "\n");
            }
        }

        private void button_ViewDeal_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            ObjectId carID = ObjectId.Parse(clicked.Tag.ToString());

            TimeSpan duration = dateTimePicker_dropoff.Value.Subtract(dateTimePicker_pickup.Value);
            string PickUpLocation = txtbox_PickUpLocation.Text;
            string DropOffLocation;
            if (checkBox_DropOff.Checked) DropOffLocation = txtbox_DropOffLocation.Text;
            else DropOffLocation = txtbox_PickUpLocation.Text;
            string PickUpDate = dateTimePicker_pickup.Text;
            string DropOffDate = dateTimePicker_dropoff.Text;
            string PickUpTime = comboBox_pickuptime.Text;
            string DropOffTime = comboBox_dropofftime.Text;
            datXeInfo datxeinfo = new datXeInfo(carID,PickUpLocation,PickUpDate,PickUpTime,DropOffLocation,DropOffDate,DropOffTime,(int)duration.TotalDays);
            datxeinfo.Show();
        }
    }
}
