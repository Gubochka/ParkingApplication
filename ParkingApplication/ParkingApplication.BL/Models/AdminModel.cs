namespace ParkingApplication.BL.Models;

public class AdminModel
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int? ParkingTemplateId { get; set; }
    public bool IsSuperAdmin { get; set; }
}