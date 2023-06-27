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
using BookingCom.model;

namespace BookingCom.pages.hoatDong
{
    public partial class HoatDong_Search : Form
    {
        public HoatDong_Search()
        {
            InitializeComponent();
        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string loc = txtLocation.Text;
            if (string.IsNullOrEmpty(loc))
            {
                MessageBox.Show("Địa điểm không thể trống.");
                return;
            }
            string ngaybd = dateFrom.Text.ToString();
            string ngaykt = dateTo.Text.ToString();

            
            DateTime parsedDateTime = DateTime.ParseExact(ngaybd, "dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
            ngaybd = parsedDateTime.ToString("yyyy-MM-dd");

            
            parsedDateTime = DateTime.ParseExact(ngaykt, "dddd, MMMM d, yyyy", CultureInfo.InvariantCulture);
            ngaykt = parsedDateTime.ToString("yyyy-MM-dd");

            List<CHoatDong> hoatDongs =  await CHoatDong.FindByLocation(loc, ngaybd, ngaykt);
            ShowSearchResults(hoatDongs);
            
        }

        private void ShowSearchResults(List<CHoatDong> results)
        {
            // Xóa các kết quả hiện tại
            flowLayoutPanelResults.Controls.Clear();

            // Tạo giao diện hiển thị kết quả
            foreach (var hoatDong in results)
            {
                // Tạo panel chứa thông tin của mỗi hoạt động
                Panel panel = new Panel
                {
                    BorderStyle = BorderStyle.FixedSingle,
                    Padding = new Padding(10),
                    Margin = new Padding(0, 0, 0, 10)
                    
                };
                panel.AutoSize = true;
                panel.MaximumSize = new Size(flowLayoutPanelResults.Width, flowLayoutPanelResults.Height);

                // Tạo các controls để hiển thị thông tin
                Label lblName = new Label
                {
                    Text = hoatDong._name,
                    AutoSize = true
                    
                };
                lblName.MaximumSize = new Size(flowLayoutPanelResults.Width, 0);

                Label lblRatings = new Label
                {
                    Text = "Ratings: " + hoatDong._ratings.ToString(),
                    AutoSize = true
                };
                lblRatings.MaximumSize = new Size(flowLayoutPanelResults.Width, 0);

                Label lblGioiThieu = new Label
                {
                    Text = hoatDong._gt,
                    AutoSize = true
                };
                lblGioiThieu.MaximumSize = new Size(flowLayoutPanelResults.Width, 0);


                // Đặt vị trí và kích thước của các controls
                lblName.Location = new Point(10, 10);
                lblRatings.Location = new Point(10, 30);
                lblGioiThieu.Location = new Point(10, 50);

                // Thêm các controls vào panel
                panel.Controls.Add(lblName);
                panel.Controls.Add(lblRatings);
                panel.Controls.Add(lblGioiThieu);

                // Thêm panel vào panel chứa kết quả
                panel.Width = flowLayoutPanelResults.Width;
                flowLayoutPanelResults.Controls.Add(panel);
            }
        }
    }
}
