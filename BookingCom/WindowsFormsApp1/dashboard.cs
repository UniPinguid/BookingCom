using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookingCom.pages;

namespace BookingCom
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }



        private void btnLuuTru_Click(object sender, EventArgs e)
        {
            luuTru luuTruPage = new luuTru();
            luuTruPage.Show();
        }

        private void btnDatXe_Click(object sender, EventArgs e)
        {
            //datXe datXePage = new datXe();
            //datXePage.Show();
        }
    }
}
