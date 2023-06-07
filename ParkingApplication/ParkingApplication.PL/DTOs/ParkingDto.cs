namespace ParkingApplication.DTOs;

public class ParkingDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int ParkingTemplateId { get; set; }
    public int FloorNumber { get; set; }
    public int SlotNumber { get; set; }
    public DateTime StandsUntil { get; set; }
    public float Price { get; set; }
}