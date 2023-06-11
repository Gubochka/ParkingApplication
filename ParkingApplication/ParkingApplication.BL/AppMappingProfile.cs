using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AdminModel, Admin>()
            .ForMember(admin => admin.Id,
                opt => opt.MapFrom(admin => admin.Id))
            .ForMember(admin => admin.Email,
                opt => opt.MapFrom(admin => admin.Email))
            .ForMember(admin => admin.Password,
                opt => opt.MapFrom(admin => admin.Password))
            .ForMember(admin => admin.ParkingTemplateId,
                opt => opt.MapFrom(admin => admin.ParkingTemplateId))
            .ForMember(admin => admin.IsSuperAdmin,
                opt => opt.MapFrom(admin => admin.IsSuperAdmin));
        
        CreateMap<Admin, AdminModel>()
            .ForMember(admin => admin.Id,
                opt => opt.MapFrom(admin => admin.Id))
            .ForMember(admin => admin.Email,
                opt => opt.MapFrom(admin => admin.Email))
            .ForMember(admin => admin.Password,
                opt => opt.MapFrom(admin => admin.Password))
            .ForMember(admin => admin.ParkingTemplateId,
                opt => opt.MapFrom(admin => admin.ParkingTemplateId))
            .ForMember(admin => admin.IsSuperAdmin,
                opt => opt.MapFrom(admin => admin.IsSuperAdmin));
        
        CreateMap<OwnerModel, Owner>()
            .ForMember(owner => owner.FullName,
                opt => opt.MapFrom(owner => owner.FullName))
            .ForMember(owner => owner.PhoneNumber,
                opt => opt.MapFrom(owner => owner.PhoneNumber));
        
        
        CreateMap<Owner, OwnerModel>()
            .ForMember(owner => owner.FullName,
                opt => opt.MapFrom(owner => owner.FullName))
            .ForMember(owner => owner.PhoneNumber,
                opt => opt.MapFrom(owner => owner.PhoneNumber));

        CreateMap<CarModel, Car>()
            .ForMember(car => car.OwnerId,
                opt => opt.MapFrom(car => car.OwnerId))
            .ForMember(car => car.CarName,
                opt => opt.MapFrom(car => car.CarName))
            .ForMember(car => car.CarNumber,
                opt => opt.MapFrom(car => car.CarNumber));
        
        CreateMap<Car, CarModel>()
            .ForMember(car => car.OwnerId,
                opt => opt.MapFrom(car => car.OwnerId))
            .ForMember(car => car.CarName,
                opt => opt.MapFrom(car => car.CarName))
            .ForMember(car => car.CarNumber,
                opt => opt.MapFrom(car => car.CarNumber));
        
        CreateMap<ParkingTemplateModel, ParkingTemplate>()
            .ForMember(parkingTemplate => parkingTemplate.Name,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Name))
            .ForMember(parkingTemplate => parkingTemplate.FloorsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.FloorsCount))
            .ForMember(parkingTemplate => parkingTemplate.SlotsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.SlotsCount))
            .ForMember(parkingTemplate => parkingTemplate.Price,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Price));
        
        CreateMap<ParkingTemplate, ParkingTemplateModel>()
            .ForMember(parkingTemplate => parkingTemplate.Name,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Name))
            .ForMember(parkingTemplate => parkingTemplate.FloorsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.FloorsCount))
            .ForMember(parkingTemplate => parkingTemplate.SlotsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.SlotsCount))
            .ForMember(parkingTemplate => parkingTemplate.Price,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Price));
        
        CreateMap<ParkingModel, Parking>()
            .ForMember(parking => parking.CarId,
                opt => opt.MapFrom(parking => parking.CarId))
            .ForMember(parking => parking.FloorNumber,
                opt => opt.MapFrom(parking => parking.FloorNumber))
            .ForMember(parking => parking.SlotNumber,
                opt => opt.MapFrom(parking => parking.SlotNumber))
            .ForMember(parking => parking.StandsUntil,
                opt => opt.MapFrom(parking => parking.StandsUntil))
            .ForMember(parking => parking.Price,
                opt => opt.MapFrom(parking => parking.Price));
        
        CreateMap<Parking, ParkingModel>()
            .ForMember(parking => parking.CarId,
                opt => opt.MapFrom(parking => parking.CarId))
            .ForMember(parking => parking.FloorNumber,
                opt => opt.MapFrom(parking => parking.FloorNumber))
            .ForMember(parking => parking.SlotNumber,
                opt => opt.MapFrom(parking => parking.SlotNumber))
            .ForMember(parking => parking.StandsUntil,
                opt => opt.MapFrom(parking => parking.StandsUntil))
            .ForMember(parking => parking.Price,
                opt => opt.MapFrom(parking => parking.Price));
    }
}