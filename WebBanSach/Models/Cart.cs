using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanSach.Models
{
    public class Cart
    {
        private List<CartItem> _cart;
        public Cart()
        {
            _cart = new List<CartItem>();
        }
        public List<CartItem> Carts
        {
            get { return _cart; }
        }

        //them san pham vao gio hang
        public void Add(CartItem item)
        {
            int index = timSanPham(item.MaSP);
            if(index == -1)
            {
                _cart.Add(item);
            }
            else
            {
                _cart[index].SoLuong += item.SoLuong;
            }
        }
        //cap nhap so luong
        public void Update(int masp, int soluong)
        {
            //tim san pham trong danh sachc masp
            int index = timSanPham(masp);
            if(index !=-1) // tim thay san pham
            {
                if(soluong > 0)
                {
                    _cart[index].SoLuong = soluong;
                }else
                {
                    _cart.RemoveAt(index);
                }
            }
        }
        private int timSanPham(int masp)
        {
            for(int i = 0; i < _cart.Count; i++)
            {
                if (_cart[i].MaSP == masp)
                    return i;
            }
            return -1;
        }
        //xoa san pham trong gio hang
        public void Delete(int masp)
        {
            int index = timSanPham(masp);
            if(index != -1)
            {
                _cart.RemoveAt(index);
            }
        }
    }
    public class CartItem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }
    }
}