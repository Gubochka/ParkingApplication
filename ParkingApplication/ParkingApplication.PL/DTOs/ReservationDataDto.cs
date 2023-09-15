namespace ParkingApplication.DTOs;

public class ReservationDataDto
{
    public OwnerDto? OwnerData { get; set; }
    public CarDto CarData { get; set; } = default!;
    public ParkingDto ParkingData { get; set; } = default!;
}