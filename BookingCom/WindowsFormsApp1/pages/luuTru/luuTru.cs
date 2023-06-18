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
    public partial class luuTru : Form
    {
        public static string connectionString = "mongodb://localhost:27017";

        public static IMongoClient client = new MongoClient(connectionString);

        public static IMongoDatabase db = client.GetDatabase("luuTruDB");

        public static IMongoCollection<Stay> collection = db.GetCollection<Stay>("stay");

        public static string selectStayId = "";

        public DateTime checkinDate;
        public DateTime checkoutDate;
        public int noNights;
        public int noAdults = 0;
        public int noChildren = 0;


        // Define a class to represent the result of the aggregation
        public class StayAggregate
        {
            public ObjectId _id { get; set; }
            public decimal CheapPrice { get; set; }
        }

        public luuTru()
        {
            InitializeComponent();

            checkinDate = DateTime.Now;
            checkoutDate = DateTime.Now.AddDays(1);

            readData();
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
        }

        public static void cloneTextBox(string oldTextBoxName, TextBox newTextBox, GroupBox groupBox)
        {
            TextBox oldTextBox = groupBox.Controls.Find(oldTextBoxName, true).FirstOrDefault() as TextBox;

            newTextBox.Font = oldTextBox.Font;
            newTextBox.ForeColor = oldTextBox.ForeColor;
            newTextBox.BackColor = oldTextBox.BackColor;
            newTextBox.Location = oldTextBox.Location;

            bool autoSize = newTextBox.AutoSize;
            newTextBox.AutoSize = autoSize;

            newTextBox.Width = oldTextBox.Width;
            newTextBox.Height = oldTextBox.Height;

            ContentAlignment textAlign = (ContentAlignment)oldTextBox.TextAlign;
            newTextBox.TextAlign = (HorizontalAlignment)textAlign;
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

        public void readData()
        {
            // Retrieve data from the "Stay" collection
            var stays = collection.Find(FilterDefinition<Stay>.Empty).ToList();

            // Clear existing GroupBoxes before populating new ones
            groupBoxesPanel.Controls.Clear();

            // Set the initial Y-coordinate position
            int groupBoxY = groupBox_stay0.Location.Y;

            // Create a GroupBox for each "Stay" record
            foreach (var stay in stays)
            {
                GroupBox groupBox = new GroupBox();

                // Set the properties of the GroupBox
                groupBox.Size = groupBox_stay0.Size;
                groupBox.Location = new Point(groupBox_stay0.Location.X, groupBoxY);

                // Create and set the properties of the labels inside the GroupBox
                Label nameLabel = new Label();
                nameLabel.Text = stay.Name;
                cloneLabel("label_name", nameLabel, groupBox_stay0);

                Label locationLabel = new Label();
                locationLabel.Text = stay.Location;
                cloneLabel("label_location", locationLabel, groupBox_stay0);

                Label descriptionLabel = new Label();
                descriptionLabel.Text = stay.Description;
                cloneLabel("label_description", descriptionLabel, groupBox_stay0);

                Label scoreLabel = new Label();
                scoreLabel.Text = stay.Score.ToString();
                cloneLabel("label_score", scoreLabel, groupBox_stay0);

                Label cheapLabel = new Label();
                cheapLabel.Text = "VND " + stay.CheapPrice.ToString();
                cloneLabel("label_cheap", cheapLabel, groupBox_stay0);

                // Add the labels to the GroupBox
                groupBox.Controls.Add(nameLabel);
                groupBox.Controls.Add(locationLabel);
                groupBox.Controls.Add(descriptionLabel);
                groupBox.Controls.Add(scoreLabel);
                groupBox.Controls.Add(cheapLabel);

                // Clone the "btn_info" button and add it to the GroupBox
                Button infoButton = new Button();
                cloneButton("btn_info", infoButton, groupBox_stay0);
                infoButton.Tag = stay.Id.ToString();
                infoButton.Click += btn_info_Click;
                groupBox.Controls.Add(infoButton);

                // Add the cloned GroupBox to the panel or container
                groupBoxesPanel.Controls.Add(groupBox);

                // Update the Y-coordinate position for the next GroupBox
                groupBoxY += 200;
            }
        }



        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            readData();
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ObjectId selectedStayId = ObjectId.Parse(clickedButton.Tag.ToString());

            luuTru_stayInfo stayInfo = new luuTru_stayInfo(selectedStayId);
            stayInfo.checkinDate = checkinDate;
            stayInfo.checkoutDate = checkoutDate;
            stayInfo.noNights = noNights;
            stayInfo.noAdults = noAdults;
            stayInfo.noChildren = noChildren;

            stayInfo.Show();
        }

        private void check_in_date_ValueChanged(object sender, EventArgs e)
        {
            checkinDate = check_in_date.Value;
        }

        private void check_out_date_ValueChanged(object sender, EventArgs e)
        {
            checkoutDate = check_out_date.Value;
        }

        private void noAdultsUpDown_ValueChanged(object sender, EventArgs e)
        {
            noAdults = (int)noAdultsUpDown.Value;
        }

        private void noChildrenUpDown_ValueChanged(object sender, EventArgs e)
        {
            noChildren = (int)noChildrenUpDown.Value;
        }
    }
}
