using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class User
    {
        public int MaKH { get; set; }
        public string HoTenKH { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string TenDN { get; set; }
        public string MatKhau { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Email { get; set; }
    }
}