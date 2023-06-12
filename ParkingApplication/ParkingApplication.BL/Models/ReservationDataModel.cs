namespace ParkingApplication.BL.Models;

public class ReservationDataModel
{
    public OwnerModel? OwnerData { get; set; }
    public CarModel CarData { get; set; }
    public ParkingModel ParkingData { get; set; }
}