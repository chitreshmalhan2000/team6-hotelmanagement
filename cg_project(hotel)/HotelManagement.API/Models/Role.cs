namespace HotelManagement.API.Models;

public partial class Role
{
    public int RoleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
}
