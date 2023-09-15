namespace ParkingApplication.DAL.Entities;

public class ParkingTemplate : BaseEntity
{
    public string Name { get; set; } = default!;
    public int FloorsCount { get; set; }
    public int SlotsCount { get; set; }
    public float Price { get; set; }
    public virtual ICollection<Admin>? Admins { get; set; } = new List<Admin>();
    public virtual ICollection<Parking>? Parkings { get; set; } = new List<Parking>();
}