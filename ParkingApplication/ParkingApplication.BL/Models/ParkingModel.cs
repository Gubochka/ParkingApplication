namespace ParkingApplication.BL.Models;

public class ParkingModel
{
    public int CarId { get; set; }
    public int FloorNumber { get; set; }
    public int SlotNumber { get; set; }
    public DateTime StandsUntil { get; set; }
    public float Price { get; set; }
}