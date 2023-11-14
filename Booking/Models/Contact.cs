using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking.Models
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nội dung bắt buộc")]
        public string? Content { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
