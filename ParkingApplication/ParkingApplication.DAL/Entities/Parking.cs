namespace ParkingApplication.DAL.Entities;

public class Parking : BaseEntity
{
    public int CarId { get; set; }
    public int FloorNumber { get; set; }
    public int SlotNumber { get; set; }
    public DateTime StandsUntil { get; set; }
    public float Price { get; set; }
    public virtual Car? Car { get; set; }
}