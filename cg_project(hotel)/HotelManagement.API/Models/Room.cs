using System;
using System.Collections.Generic;

namespace HotelManagement.API.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public int? RoomNumber { get; set; }

    public int? RoomTypeId { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual RoomType? RoomType { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}
