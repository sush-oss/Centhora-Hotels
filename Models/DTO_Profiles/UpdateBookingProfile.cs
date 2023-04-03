using AutoMapper;
using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.Models.DTO_Profiles
{
    public class UpdateBookingProfile : Profile
    {
        public UpdateBookingProfile() 
        {
            CreateMap<Booking, UpdateBookingDto>()
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.CheckIn_Date, opt => opt.MapFrom(src => src.CheckIn_Date))
                .ForMember(dest => dest.CheckOut_Date, opt => opt.MapFrom(src => src.CheckOut_Date))
                .ForMember(dest => dest.NumOfRoomsBooked, opt => opt.MapFrom(src => src.NumOfRoomsBooked));

            CreateMap<UpdateBookingDto, Booking>()
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.CheckIn_Date, opt => opt.MapFrom(src => src.CheckIn_Date))
                .ForMember(dest => dest.CheckOut_Date, opt => opt.MapFrom(src => src.CheckOut_Date))
                .ForMember(dest => dest.NumOfRoomsBooked, opt => opt.MapFrom(src => src.NumOfRoomsBooked));
        }
    }
}
