namespace ParkingApplication.DTOs;

public class ReservationDataDto
{
    public OwnerDto? OwnerData { get; set; }
    public CarDto CarData { get; set; }
    public ParkingDto ParkingData { get; set; }
}