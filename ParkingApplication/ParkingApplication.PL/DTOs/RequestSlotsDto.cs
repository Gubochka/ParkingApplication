namespace ParkingApplication.DTOs;

public class RequestSlotsDto
{
    public int ParkingId { get; set; }
    public int Floor { get; set; }
    public int? Slot { get; set; }
}