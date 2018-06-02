using LazadaLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazadaLatest.ViewModels
{
    public class ProductCartVM
    {
        public User User { set; get; }
        public List<Product> Products { set; get; }
        public int totalQuantity { set; get; }
        public double totalPrice { set; get; }
    }
}