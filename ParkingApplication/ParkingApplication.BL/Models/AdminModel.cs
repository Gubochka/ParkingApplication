namespace ParkingApplication.BL.Models;

public class AdminModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int? ParkingTemplateId { get; set; }
    public bool IsSuperAdmin { get; set; }
}