namespace ParkingApplication.DAL.Entities;

public class Car : BaseEntity
{
    public int OwnerId { get; set; }
    public string CarName { get; set; } = default!;
    public string CarNumber { get; set; } = default!;
    public virtual ICollection<Parking>? Parking { get; set; } = new List<Parking>();
    public virtual Owner? Owner { get; set; }
}