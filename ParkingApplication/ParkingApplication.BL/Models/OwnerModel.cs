namespace ParkingApplication.BL.Models;

public class OwnerModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}