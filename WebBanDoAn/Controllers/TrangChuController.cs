using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDoAn.Models;

namespace WebBanDoAn.Controllers
{
    public class TrangChuController : Controller
    {
        //Tạo 1 đối tượng chứa toàn bộ CSDL từ dbQLBanDoAn
        DataClassesBanDoAnDataContext data = new DataClassesBanDoAnDataContext();
        private List<SanPham> LayMonMoi(int count)
        {
            //Sắp xếp giảm dần theo giá, lấy count dòng đầu
            return data.SanPhams.OrderByDescending(a => a.TenSanPham).Take(count).ToList();
            
        }
        // GET: TrangChu
        public ActionResult Index()
        {
            //Lấy món
            var monMoi = LayMonMoi(2);
            return View(monMoi);
        }
        public ActionResult LoaiSanPham()
        {
            var loaiSP = from sp in data.LoaiSanPhams select sp;
            return PartialView(loaiSP);
        }
        public ActionResult QCSanPham()
        {
            var qcSP = from qc in data.QuangCaoSPs select qc;
            return PartialView(qcSP);
        }
        public ActionResult SPTheoLoaiSP(int id)
        {
            var mon = from m in data.SanPhams where m.IDLoaiSanPham == id select m;
            return View(mon);
        }
        public ActionResult SPTheoQCSP(int id)
        {
            var mon = from m in data.SanPhams where m.IDQuangCao == id select m;
            return View(mon);
        }
        public ActionResult Details(int id)
        {
            var mon = from m in data.SanPhams
                      where m.IDSanPham == id
                      select m;
            return View(mon.Single());
        }
    }
}