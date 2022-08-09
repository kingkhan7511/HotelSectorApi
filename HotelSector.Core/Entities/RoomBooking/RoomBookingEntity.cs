using HotelSector.Core.Entities.Room;
using HotelSector.Core.Entities.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSector.Core.Entities.RoomBooking
{
    [Table("RoomBooking")]
    public class RoomBookingEntity : BaseEntity
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int HowManyHours { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalCharges { get; set; }

        public long RoomId { get; set; }

        [ForeignKey("RoomId")]
        public RoomEntity RoomFK { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity UserFK { get; set; }
        
    }
}
