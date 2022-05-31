using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoAn.Models;

namespace WebBanDoAn.Controllers
{
    public class GioHangController : Controller
    {
        //Tạo 1 đối tượng chứa toàn bộ CSDL từ dbQLBanDoAn
        DataClassesBanDoAnDataContext data = new DataClassesBanDoAnDataContext();

        //Lấy Giỏ Hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if(listGH == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì khởi tạo list giỏ hàng
                listGH = new List<GioHang>();
                Session["GioHang"] = listGH;
            }
            return listGH;
        }
        //Thêm hàng vào giỏ
        public ActionResult ThemGioHang(int idSP, string strURL)
        {
            //Lấy ra Session giỏ hàng
            List<GioHang> listGH = LayGioHang();
            //Kiểm tra sản phẩm này tồn tại trong Session["GioHang"] chưa?
            GioHang sanPham = listGH.Find(n => n.idSanPham == idSP);
            if(sanPham == null)
            {
                sanPham = new GioHang(idSP);
                listGH.Add(sanPham);
                return Redirect(strURL);
            }    
            else
            {
                sanPham.soLuong++;
                return Redirect(strURL);
            }    
        }
        //Tổng số lượng
        private int TongSoLuong()
        {
            int tongSoLuong = 0;
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if(listGH != null)
            {
                tongSoLuong = listGH.Sum(n => n.soLuong);
            }    
            return tongSoLuong;
        }
        //Tính tổng tiền
        private double TongTien()
        {
            double tongTien = 0;
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if(listGH != null)
            {
                tongTien = listGH.Sum(n => n.thanhTien);
            }    
            return tongTien;
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            List<GioHang> listGH = LayGioHang();
            if(listGH.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.tongSoLuong = TongSoLuong();
            ViewBag.tongTien = TongTien();
            return View(listGH);
        }
        //Tạo Partial View để hiển thị thông tin giỏ hàng
        public ActionResult GioHangPartial()
        {
            ViewBag.tongSoLuong = TongSoLuong();
            ViewBag.tongTien = TongTien();
            return PartialView();
        }
    }
}