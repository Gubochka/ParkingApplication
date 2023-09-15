﻿namespace ParkingApplication.DTOs;

public class CarDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string CarName { get; set; } = default!;
    public string CarNumber { get; set; } = default!;
}