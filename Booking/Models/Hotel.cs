﻿using Booking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Booking.Models
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
        public string? HotelName { get; set; }

        [Display(Name = "Address")]
        [Column(TypeName = "nvarchar(250)")]
        [AllowNull]
        public string Address { get; set; }


        [Display(Name = "Description")]
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string? PhotoPath { get; set; }

        public DateTime? PostingDate { get; set; }
        public DateTime? ApprovalDate { get; set; }

        /*tham chiếu khóa ngoại*/
        public string? PosterID { get; set; }
        public string? ApproverID { get; set; }

        /*tham chiếu khóa ngoại*/
        public int? CityId { get; set; }
        public City? City { get; set; }

        public AppUser? Poster { get; set; }
        public AppUser? Approver { get; set; }

        public ICollection<Room>? Rooms { get; set; }

        public ICollection<HasService>? HasServices { get; set; }    
    }
}