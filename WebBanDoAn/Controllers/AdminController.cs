using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoAn.Models;

namespace WebBanDoAn.Controllers
{
    public class AdminController : Controller
    {
        DataClassesBanDoAnDataContext data = new DataClassesBanDoAnDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            //Gán giá trị người dùng nhập liệu cho các biến
            var tenDN = collection["username"];
            var matKhau = collection["password"];
            if(String.IsNullOrEmpty(tenDN))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập!";
            }
            else if(String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu!";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (AD)
                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tenDN && n.PassAdmin == matKhau);
                if(ad != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công!";
                    Session["TaiKhoanAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }    
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }    
            }
            return View();
        }
    }
}