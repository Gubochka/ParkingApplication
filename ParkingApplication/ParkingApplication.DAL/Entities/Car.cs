namespace ParkingApplication.DAL.Entities;

public class Car : BaseEntity
{
    public int OwnerId { get; set; }
    public string CarName { get; set; }
    public string CarNumber { get; set; }
    public virtual ICollection<Parking>? Parking { get; set; }
    public virtual Owner? Owner { get; set; }
}