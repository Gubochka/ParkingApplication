namespace ParkingApplication.DTOs;

public class ParkingTemplateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int FloorsCount { get; set; }
    public int SlotsCount { get; set; }
    public float Price { get; set; }
}