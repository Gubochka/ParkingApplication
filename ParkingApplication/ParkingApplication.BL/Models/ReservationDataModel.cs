namespace ParkingApplication.BL.Models;

public class ReservationDataModel
{
    public OwnerModel? OwnerData { get; set; }
    public CarModel CarData { get; set; } = default!;
    public ParkingModel ParkingData { get; set; } = default!;
}