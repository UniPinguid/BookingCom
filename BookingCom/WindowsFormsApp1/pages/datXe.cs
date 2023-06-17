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

namespace BookingCom.pages
{
    public partial class datXe : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("datXeDB");
        public datXe()
        {
            InitializeComponent();
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
    }
}
