namespace ParkingApplication.BL.Models;

public class CarModel
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string CarName { get; set; }
    public string CarNumber { get; set; }
}