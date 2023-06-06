using AutoMapper;
using ParkingApplication.BL.Models;
using ParkingApplication.DAL.Entities;

namespace ParkingApplication.BL;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
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