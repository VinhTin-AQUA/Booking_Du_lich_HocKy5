﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("Package")]
    public class Package
    {
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public int PackageID { get; set; }

       
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên gói")]
        public string PackageName { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Số người tối đa")]
        public int MaxPeople { get; set; }

        //--
        public int TourID { get; set; }

        public Tour Tour { get; set; }

        public ICollection<PackagePrice>? PackagePrices { get; set; }
        public ICollection<BookTour>? BookTours { get; set; }
    }
}
