namespace ParkingApplication.DAL.Entities;

public class Owner : BaseEntity
{
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Car>? Cars { get; set; } = new List<Car>();
}