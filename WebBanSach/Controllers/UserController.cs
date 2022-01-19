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
    public class UserController : Controller
    {
        BanSsachDBContext db = new BanSsachDBContext();
        // GET: User
        public ActionResult Index()
        {
            if(Session["TenDN"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register1( KhachHang _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Matkhau = GetMD5(_user.Matkhau);//
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(_user);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View("Register");
                }
            }
            //them vao CSDL
            db.KhachHangs.Add(_user);
            db.SaveChanges();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login1(KhachHang _user, string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);//
                var data = db.KhachHangs.Where(s => s.Email.Equals(email) && s.Matkhau.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["HoTenKH"] = data.FirstOrDefault().HoTenKH;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["MaKH"] = data.FirstOrDefault().MaKH;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login Failed";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        //public static string GetMD5(string cc)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] fromData = Encoding.UTF8.GetBytes(cc);
        //    byte[] targetData = md5.ComputeHash(fromData);
        //    string byte2String = null;
        //    for(int i = 0; i < targetData.Length; i++)
        //    {
        //        byte2String += targetData[i].ToString("x2");
        //    }
        //    return byte2String;
        //}
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}