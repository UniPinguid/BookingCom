using BookingCom.model;
using MongoDB.Bson;
using MongoDB.Driver;
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
using WindowsFormsApp1.pages;
using BookingCom.pages;
using BookingCom;

namespace WindowsFormsApp1.pages.datXe
{
    public partial class datXePayment : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("datXeDB");
        public static IMongoCollection<Car> collection_car = db.GetCollection<Car>("Car");
        public static IMongoCollection<BookingCar> collection_booking = db.GetCollection<BookingCar>("Booking Car");
        public datXePayment(ObjectId carId, string pickuplocation, string pickupdate, string pickuptime, string dropofflocation, string dropoffdate, string dropofftime, int price)
        {
            InitializeComponent();

            var filter_car = Builders<Car>.Filter.Eq(c => c.Id, carId);
            var car = collection_car.Find(filter_car).FirstOrDefault();

            if (car != null)
            {
                label_CarName.Text = car.CarBrand;
                label_CarRentalProvider.Text = car.CarRentalProvider.Name;
                label_Seats.Text = car.NumberOfSeats.ToString() + " Seats";
                label_Miles.Text = car.NumberOfMiles.ToString() + " Miles";
                label_Transmission.Text = car.Transmission;
                label_Luggages.Text = car.Luggages;
            }    

            label_Price.Text = price.ToString();
            DateTime pickup = DateTime.Parse(pickupdate);
            DateTime dropoff = DateTime.Parse(dropoffdate);
            TimeSpan duration = dropoff.Subtract(pickup);
            label_Duration.Text = "Price for " + ((int)duration.TotalDays).ToString() + " days";

            label_PickUpLocation.Text = pickuplocation;
            label_PickUpDateTime.Text = pickupdate + " " + pickuptime;
            label_DropOffLocation.Text = dropofflocation;
            label_DropOffDateTime.Text = dropoffdate + " " + dropofftime;
            button_Book.Tag = carId.ToString();
        }

        private void button_Book_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            ObjectId carid = ObjectId.Parse(clicked.Tag.ToString());
            string format = "dddd, MMMM d, yyyy HH:mm";
            DateTime pickup_datetime = DateTime.ParseExact(label_PickUpDateTime.Text, format, CultureInfo.InvariantCulture);
            DateTime dropoff_datetime = DateTime.ParseExact(label_DropOffDateTime.Text, format, CultureInfo.InvariantCulture);
            var filter = Builders<Car>.Filter.Eq(c => c.Id, carid);
            Car car_result = collection_car.Find(filter).FirstOrDefault();
            var maindriver = new MainDriver
            {
                Email = textBox_DriverEmail.Text,
                FirstName = textBox_DriverFirstName.Text,
                LastName = textBox_DriverLastName.Text,
                ContactPhone = textBox_DriverPhone.Text,
                Title = textBox_DriverTitle.Text,
                Age = int.Parse(textBox_DriverAge.Text),
                CountryOfResidence = textBox_DriverCountry.Text
            };

            var billingaddress = new BillingAddress
            {
                FirstName = textBox_BillingFirstName.Text,
                LastName = textBox_BillingLastName.Text,
                ContactNumber = textBox_BillingPhone.Text,
                Country = textBox_BillingCountry.Text,
                Address = textBox_BillingAddress.Text,
                City = textBox_BillingCity.Text,
                Postcode = int.Parse(textBox_Postcode.Text),
                BusinessBooking = checkBox_BusinessBooking.Checked
            };

            format = "MM/yy";
            DateTime expiry = DateTime.ParseExact(textBox_ExpiryDate.Text, format, CultureInfo.InvariantCulture);
            var payment = new Payment
            {
                CardHolderName = textBox_CardHolderName.Text,
                CardNumber = textBox_CardNumber.Text,
                ExpiryDate = expiry,
                CVC = textBox_CVC.Text,
            };
            var bookingcar = new BookingCar
            {
                Pickup_Location = label_PickUpLocation.Text,
                Dropoff_Location = label_DropOffLocation.Text,
                Pickup_DateTime = pickup_datetime,
                Dropoff_DateTime = dropoff_datetime,
                Car = car_result,
                MainDriver = maindriver,
                BillingAddress = billingaddress,
                Payment = payment,
                Price = int.Parse(label_Price.Text)
            };
            try
            {
                collection_booking.InsertOne(bookingcar);
                MessageBox.Show("Booking succeed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking failed.");
            }
            this.Close();
        }
    }
}
