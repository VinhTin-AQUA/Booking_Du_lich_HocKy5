using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    [Table("Room")]
    public class Room
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Number Room")]
        [Column(TypeName = "nvarchar(250)")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Room Name")]
        [Column(TypeName = "nvarchar(250)")]
        public string RoomName { get; set; }

        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Column(TypeName = "varchar(250)")]
        public string? PhotoPath { get; set; }

        // Tham chieu khoa ngoai

        public int? HotelId { get; set; }

        public Hotel Hotel { get; set; }

        public int? RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public RoomPrice RoomPrice { get; set; }

        public int? RoomId { get; set; }

        public DateTime? ValidFrom { get; set; }

        public BookRoom BookRoom { get; set; }
    }
}
