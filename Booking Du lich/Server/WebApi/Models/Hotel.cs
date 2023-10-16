﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Models
{
    [Table("Hotel")]
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} must be required")]
        [Display(Name = "Hotel name")]
        [Column(TypeName = "nvarchar(250)")]
        public string HotelName { get; set; }

        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        public string? Address { get; set; }

        public int AvailableRoom { get; set; } = 0;

        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? PhotoPath { get; set; }

        /*tham chiếu khóa ngoại*/ 
        public ICollection<ApplicationUser> Agents { get; set; }

        public int? CityId { get; set; }
        public City City { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}