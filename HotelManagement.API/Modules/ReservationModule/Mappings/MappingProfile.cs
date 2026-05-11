using AutoMapper;
using HotelManagement.API.Modules.ReservationModule.DTOs;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.ReservationModule.Mappings;

public partial class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateReservationDto, Reservation>()
            .ForMember(
                dest => dest.GuestPhone,
                opt => opt.MapFrom(src => src.GuestPhoneNumber));
    }
}