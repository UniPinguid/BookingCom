using BookingCom.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.pages.hoatDong
{
    public partial class HoatDong_ChiTiet : Form
    {
        private CHoatDong hoatDong;
        public HoatDong_ChiTiet()
        {
            InitializeComponent();
        }

        public HoatDong_ChiTiet(CHoatDong _hd)
        {
            InitializeComponent();
            hoatDong = _hd;
        }
    }
}
