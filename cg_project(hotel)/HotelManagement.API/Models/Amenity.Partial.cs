namespace HotelManagement.API.Models;

public partial class Amenity
{
    public string DisplayName => $"{Name} ({AmenityId})";
}
