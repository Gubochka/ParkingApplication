namespace ParkingApplication.DAL.Entities;

public class Owner : BaseEntity
{
    public string FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Car>? Cars { get; set; }
}