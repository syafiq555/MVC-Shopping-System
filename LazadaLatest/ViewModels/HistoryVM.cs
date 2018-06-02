using LazadaLatest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazadaLatest.ViewModels
{
    public class HistoryVM
    {
        public List<ProductCart> Products { set; get; }
        public User User { set; get; }
        public DateTime purchaseDate { set; get; }
        public int totalQuantity { set; get; }
        public decimal totalPrice { set; get; }

    }
}