using BookingCom.model;
using BookingCom.pages;
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

namespace WindowsFormsApp1.pages.luuTru
{
    public partial class luuTru_booking : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("luuTruDB");

        public static IMongoCollection<Stay> stayCollection = db.GetCollection<Stay>("stay");

        public string stayIdString;
        public DateTime checkinDate;
        public DateTime checkoutDate;
        public int noAdults;
        public int noChildren;

        public luuTru_booking()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            label_checkin.Text = checkinDate.ToString("dd/MM/yyyy");
        }

        private void paymentMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            label_alert_payment.Dispose();
        }
    }
}
