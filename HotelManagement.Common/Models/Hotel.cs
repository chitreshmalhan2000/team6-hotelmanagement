using System;
using System.Collections.Generic;

namespace HotelManagement.Common.Models;

public partial class Hotel
{
    public int HotelId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
}
