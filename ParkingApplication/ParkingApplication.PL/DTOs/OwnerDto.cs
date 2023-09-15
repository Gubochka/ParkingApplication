namespace ParkingApplication.DTOs;

public class OwnerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}