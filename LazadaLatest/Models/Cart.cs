//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LazadaLatest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cart()
        {
            this.ProductCarts = new HashSet<ProductCart>();
        }

        [Display(Name = "Cart ID")]
        public int Id { get; set; }

        [Display(Name = "User ID")]
        public Nullable<int> userId { get; set; }

        [Display(Name = "Total Price (RM)")]
        public Nullable<double> totalPrice { get; set; }

        [Display(Name = "Total Quantity")]
        public Nullable<int> totalQuantity { get; set; }
        public string status { get; set; }

        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductCart> ProductCarts { get; set; }
    }
}
