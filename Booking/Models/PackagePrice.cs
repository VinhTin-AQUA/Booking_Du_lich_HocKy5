using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
{
    [Table("PackagePrice")]
    public class PackagePrice
    {
        [Key]
        public int PriceId { get; set; }

        [Display(Name = "Giá cho người lớn")]
        [Range(0, 999999999, ErrorMessage = "{0} phải là số dương")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "{0} phải là số")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public double AdultPrice { get; set; }

        [Display(Name = "Giá cho trẻ nhỏ")]
        [Range(0, 999999999, ErrorMessage = "{0} phải là số dương")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "{0} phải là số")]
        [Required(ErrorMessage = "{0} không được bỏ trống")]
        public double ChildPrice { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày áp dụng")]
        public DateTime? ValidFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ngày kết thúc")]
        public DateTime? GoodThru { get; set; }

        public int PackageId { get; set; }

        [AllowNull]
        public Package? Package { get; set; }
    }
}
