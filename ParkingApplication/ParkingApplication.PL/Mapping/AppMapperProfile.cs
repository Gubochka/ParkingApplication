using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.DTOs;

namespace ParkingApplication.Mapping;

public class AppMapperProfile : Profile
{
    public AppMapperProfile()
    {
        CreateMap<AdminDto, AdminModel>()
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
        
        CreateMap<AdminModel, AdminDto>()
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
        
        CreateMap<OwnerDto, OwnerModel>()
            .ForMember(owner => owner.FullName,
                opt => opt.MapFrom(owner => owner.FullName))
            .ForMember(owner => owner.PhoneNumber,
                opt => opt.MapFrom(owner => owner.PhoneNumber));
        
        
        CreateMap<OwnerModel, OwnerDto>()
            .ForMember(owner => owner.Id,
                opt => opt.MapFrom(owner => owner.Id))
            .ForMember(owner => owner.FullName,
                opt => opt.MapFrom(owner => owner.FullName))
            .ForMember(owner => owner.PhoneNumber,
                opt => opt.MapFrom(owner => owner.PhoneNumber));

        CreateMap<CarDto, CarModel>()
            .ForMember(car => car.Id,
                opt => opt.MapFrom(car => car.Id))
            .ForMember(car => car.OwnerId,
                opt => opt.MapFrom(car => car.OwnerId))
            .ForMember(car => car.CarName,
                opt => opt.MapFrom(car => car.CarName))
            .ForMember(car => car.CarNumber,
                opt => opt.MapFrom(car => car.CarNumber));
        
        CreateMap<CarModel, CarDto>()
            .ForMember(car => car.Id,
                opt => opt.MapFrom(car => car.Id))
            .ForMember(car => car.OwnerId,
                opt => opt.MapFrom(car => car.OwnerId))
            .ForMember(car => car.CarName,
                opt => opt.MapFrom(car => car.CarName))
            .ForMember(car => car.CarNumber,
                opt => opt.MapFrom(car => car.CarNumber));
        
        CreateMap<ParkingTemplateDto, ParkingTemplateModel>()
            .ForMember(parkingTemplate => parkingTemplate.Id,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Id))
            .ForMember(parkingTemplate => parkingTemplate.Name,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Name))
            .ForMember(parkingTemplate => parkingTemplate.FloorsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.FloorsCount))
            .ForMember(parkingTemplate => parkingTemplate.SlotsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.SlotsCount))
            .ForMember(parkingTemplate => parkingTemplate.Price,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Price));
        
        CreateMap<ParkingTemplateModel, ParkingTemplateDto>()
            .ForMember(parkingTemplate => parkingTemplate.Id,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Id))
            .ForMember(parkingTemplate => parkingTemplate.Name,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Name))
            .ForMember(parkingTemplate => parkingTemplate.FloorsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.FloorsCount))
            .ForMember(parkingTemplate => parkingTemplate.SlotsCount,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.SlotsCount))
            .ForMember(parkingTemplate => parkingTemplate.Price,
                opt => opt.MapFrom(parkingTemplate => parkingTemplate.Price));
        
        CreateMap<ParkingDto, ParkingModel>()
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
        
        CreateMap<ParkingModel, ParkingDto>()
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