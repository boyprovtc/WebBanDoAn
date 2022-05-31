using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoAn.Models;
namespace WebBanDoAn.Controllers
{
    
    public class NguoiDungController : Controller
    {
        //Tạo 1 đối tượng chứa toàn bộ CSDL từ dbQLBanDoAn
        DataClassesBanDoAnDataContext data = new DataClassesBanDoAnDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        //POST: Hàm DangKy(post) nhận dữ liệu từ trang DangKy
        //và thực hiện việc tạo mới dữ liệu.
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến 
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matKhau = collection["MatKhau"];
            var nhapLaiMK = collection["NhapLaiMK"];
            var ngaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var dienThoai = collection["DienThoai"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống!";
            }
            else if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập!";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi3"] = "Nhập mật khẩu!";
            }
            else if (String.IsNullOrEmpty(nhapLaiMK))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu!";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống!";
            }
            if (String.IsNullOrEmpty(dienThoai))
            {
                ViewData["Loi6"] = "Phải nhập số điện thoại!";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi7"] = "Nhập địa chỉ!";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (KH)
                kh.HoTenKhachHang = hoten;
                kh.TaiKhoan = tendn;
                kh.MatKhau = matKhau;
                kh.Email = email;
                kh.DiaChi = diachi;
                kh.SoDienThoai = dienThoai;
                kh.NgaySinh = DateTime.Parse(ngaySinh);
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDN"];
            var matKhau = collection["MatKhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }    
            else if(String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }    
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.MatKhau == matKhau);
                if(kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    ViewBag.Thongbao = "Đăng nhập thành công!!!";
                }
                else
                {
                    ViewBag.Thongbao = " Tên đăng nhập hoặc mật khẩu không đúng!!!";
                }
            }
            return View();
        }
    }
}