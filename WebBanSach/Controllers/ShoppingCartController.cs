using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanSach.Models;
using PagedList;
using System.Security.Cryptography;
using System.Text;

namespace WebBanSach.Controllers
{
    public class ShoppingCartController : Controller
    {
        BanSsachDBContext db = new BanSsachDBContext();
        // GET: ShoppingCart
        public ActionResult AddToCart(int id)
        {
            //truy van thong tin sach tu CSDL
            var sach = db.Saches.Find(id);
            Cart cart = (Cart)Session["GIOHANG"];
            if(cart == null)
            {
                cart = new Cart();
                Session["GIOHANG"] = cart;
            }
            cart.Add(new CartItem
            {
                MaSP = sach.MaSach,
                TenSP = sach.TenSach,
                HinhAnh = sach.AnhBia,
                DonGia = (double)sach.Dongia,
                SoLuong = 1
            });
            return RedirectToAction("ViewCart");
        }
        public ActionResult Update(int MaSP, int Soluong)
        {
            Cart cart = (Cart)Session["GIOHANG"];
            cart.Update(MaSP, Soluong);
            return RedirectToAction("ViewCart");
        }
        public ActionResult Delete(int id)
        {
            Cart cart = (Cart)Session["GIOHANG"];
            cart.Delete(id);
            return RedirectToAction("ViewCart");
        }
        public ActionResult DeleteAll()
        {
            Session.Clear();
            return RedirectToAction("ViewCart");
        }
        public ActionResult ViewCart()
        {
            Cart cart = (Cart)Session["GIOHANG"];
            if(cart == null)
            {
                cart = new Cart();
                Session["GIOHANG"] = cart;
            }
            return View(cart);
            
        }
        public ActionResult Checkout()
        {
            ViewCart();
            return View();
        }
    }
}