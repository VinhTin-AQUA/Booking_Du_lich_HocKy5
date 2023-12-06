using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking.Models
{
    [Table("Category")]
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "{0} không được bỏ trống")]
        [Display(Name = "Tên loại tour")]
        [Column(TypeName = "nvarchar(500)")]
        public string CategoryName { get; set; }

        public ICollection<TourCategory>? TourCategories { get; set; }
    }
}
