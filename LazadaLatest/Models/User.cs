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

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Carts = new HashSet<Cart>();
        }

        public int userId { get; set; }

        [Required(ErrorMessage = "Username cannot be empty")]
        //[Range(1, 50, ErrorMessage = "Username is too long, please choose a username in range between 1-50")]
        [StringLength(maximumLength:20, MinimumLength = 3, ErrorMessage = "Username is invalid, username must be in range between 3-20 characters")]
        [Display(Name = "Username")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        //[Range(1, 50, ErrorMessage = "Password is too long, please choose a password in range between 1-50")]
        [StringLength(50, ErrorMessage = "Password is invalid, please choose a password must be in range between 3-20 characters")]
        [Display(Name = "Password")]
        public string userPass { get; set; }

        [Display(Name = "Role")]
        public string userRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }
    }
}