namespace ParkingApplication.BL.Models;

public class ParkingTemplateModel
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int FloorsCount { get; set; }
    public int SlotsCount { get; set; }
    public float Price { get; set; }
}