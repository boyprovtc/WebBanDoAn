using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanDoAn.Models
{
    public class GioHang
    {
        //Tao doi tuong data chua dữ liệu từ model dbBanDoAn đã tạo. 
        DataClassesBanDoAnDataContext data = new DataClassesBanDoAnDataContext();
        public int idSanPham { set; get; }
        public string tenSP { set; get; }
        public string hinhAnh { set; get; }
        public Double donGia { set; get; }
        public int soLuong { set; get; }
        public Double thanhTien
        {
            get { return soLuong * donGia; }

        }
        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public GioHang(int idSP)
        {
            idSanPham = idSP;
            SanPham sp = data.SanPhams.Single(n => n.IDSanPham == idSanPham);
            tenSP = sp.TenSanPham;
            hinhAnh = sp.HinhAnh;
            donGia = double.Parse(sp.DonGia.ToString());
            soLuong = 1;
        }
    }
}