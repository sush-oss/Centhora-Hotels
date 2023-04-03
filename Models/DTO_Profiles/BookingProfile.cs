using AutoMapper;
using Centhora_Hotels.Models.DTO;

namespace Centhora_Hotels.Models.DTO_Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile() 
        {
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.CheckIn_Date, opt => opt.MapFrom(src => src.CheckIn_Date))
                .ForMember(dest => dest.CheckOut_Date, opt => opt.MapFrom(src => src.CheckOut_Date))
                .ForMember(dest => dest.NumOfRoomsBooked, opt => opt.MapFrom(src => src.NumOfRoomsBooked));

            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.CheckIn_Date, opt => opt.MapFrom(src => src.CheckIn_Date))
                .ForMember(dest => dest.CheckOut_Date, opt => opt.MapFrom(src => src.CheckOut_Date))
                .ForMember(dest => dest.NumOfRoomsBooked, opt => opt.MapFrom(src => src.NumOfRoomsBooked));
        }
    }
}
