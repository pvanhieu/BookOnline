using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
using PagedList;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebBanSach.Controllers
{
    public class HomeController : Controller
    {
        BanSsachDBContext db = new BanSsachDBContext();
        // Liet ke sach moi.
        public ActionResult Index()
        {

            var sachmoi = db.Saches.OrderByDescending(x => x.Ngaycapnhat).Take(7).ToList();
            //if (Session["MaKH"] != null)
            //{
            //    return View(sachmoi);
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}

            return View(sachmoi);
        }
        public ActionResult LietKeChuDe()
        {
            //var chude = db.ChuDes.ToList();
            return PartialView(db.ChuDes.OrderBy(x => x.Tenchude).ToList());

        }

        public ActionResult XemSachTheoChuDe(int id, int? page)
        {
            var dsSach = db.Saches.Where(x => x.MaCD == id).ToList();
            var ten = db.ChuDes.Find(id).Tenchude;
            ViewBag.TenChuDe = ten;
            ViewBag.MaCD = id;
            int pageSize = 6;
            int pageNumber = page ?? 1;
            return View(dsSach.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Chitiet(int id)
        {
            var dsSach = db.Saches.Find(id);
            return View(dsSach);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(KhachHang model,string returnUrl)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = db.KhachHangs.FindAsync(model.TenDN, model.Matkhau);
        //        if(user != null)
        //        {
        //            return View("Login");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "");
        //        }
        //    }
        //    return View(model);
        //}
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //
        // POST: /Account/Register
        

    }
}