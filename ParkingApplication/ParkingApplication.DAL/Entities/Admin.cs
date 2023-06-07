namespace ParkingApplication.DAL.Entities;

public class Admin : BaseEntity
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int? ParkingTemplateId { get; set; }
    public bool IsSuperAdmin { get; set; }
    public virtual ParkingTemplate? ParkingTemplate { get; set; }
}