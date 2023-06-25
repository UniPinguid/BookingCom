using BookingCom.model;
using MongoDB.Bson;
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
using WindowsFormsApp1.pages.datXe;

namespace WindowsFormsApp1.pages
{
    public partial class datXeInfo : Form
    {
        public datXeInfo(ObjectId CarID, string pickuplocation, string pickupdate, string pickuptime, string dropofflocation, string dropoffdate, string dropofftime, int duration)
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";

            IMongoClient client = new MongoClient(connectionString);

            IMongoDatabase db = client.GetDatabase("datXeDB");
            IMongoCollection<Car> collection = db.GetCollection<Car>("Car");

            var filter = Builders<Car>.Filter.Eq(c=> c.Id,CarID);
            
            var car = collection.Find(filter).FirstOrDefault();

            if (car != null)
            {
                label_CarName.Text = car.CarBrand;
                label_CarRentalProvider.Text = car.CarRentalProvider.Name;
                label_Seats.Text = car.NumberOfSeats.ToString() + " Seats";
                label_Miles.Text = car.NumberOfMiles.ToString() + " Miles";
                label_Transmission.Text ="Transmission: " + car.Transmission.ToString();
                label_Luggages.Text = car.Luggages.ToString();
                label_Price.Text = "đ " + (car.Price * duration).ToString();
                groupBox_Price.Text = "Car Price For " + duration.ToString() + " days";
                label_CarType.Text = car.CarType.ToString();
                label_FuelPolicy.Text = car.FuelPolicy.ToString();
                label_Information.Text = car.Information.ToString();
                label_ElectricCar.Text = car.ElectricCar.ToString();
                label_PriceCar.Text = "đ " + car.Price.ToString();
            }
            label_PickUpLocation.Text = pickuplocation;
            label_PickUpDate.Text = pickupdate;
            label_PickUpTime.Text = pickuptime;
            label_DropOffLocation.Text = dropofflocation;
            label_DropOffDate.Text = dropoffdate;
            label_DropOffTime.Text = dropofftime;
            button_Payment.Tag = CarID.ToString();
        }

        private void button_Payment_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            ObjectId carid = ObjectId.Parse(clicked.Tag.ToString());

            string pickuplocation = label_PickUpLocation.Text;
            string pickupdate = label_PickUpDate.Text;
            string pickuptime = label_PickUpTime.Text;
            string dropofflocation = label_DropOffLocation.Text;
            string dropoffdate = label_DropOffDate.Text;
            string dropofftime = label_DropOffTime.Text;
            string price = label_Price.Text;

            datXePayment datxepayment = new datXePayment(carid,pickuplocation,pickupdate,pickuptime,dropofflocation,dropoffdate,dropofftime,int.Parse(price.Substring(2)));
            datxepayment.ShowDialog();
        }
    }
}
