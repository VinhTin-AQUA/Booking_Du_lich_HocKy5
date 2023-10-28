using System.ComponentModel.DataAnnotations;

namespace WebApi.DTOs.BookRoom
{
    public class AddBookRoomDTO
    {
        
        [Required(ErrorMessage = "{0} must be required")]
        public string UserID { get; set; }
     
        [Required(ErrorMessage = "{0} must be required")]
        public int RoomID { get; set; }
      
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? CheckInDate { get; set; }
        [Required(ErrorMessage = "{0} must be required")]
        public DateTime? CheckOutDate { get; set; }

       
    }
}
