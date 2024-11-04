using AutoMapper.Execution;
using CrocusFitnes.Dto_s;
using CrocusFitnes.Entities;

namespace CrocusFitnes;

using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Member, MemberReadInfo>();
        CreateMap<MemberCreateInfo, Member>();
        CreateMap<MemberUpdateInfo, Member>();

        CreateMap<Trainers, TrainerReadInfo>();
        CreateMap<TrainerCreateInfo, Trainers>();
        CreateMap<TrainerUpdateInfo, Trainers>();


        CreateMap<Session, SessionReadInfo>();
        CreateMap<SessionCreateInfo, Session>();
        CreateMap<SessionUpdateInfo, Session>();

        CreateMap<Booking, BookingReadInfo>();
        CreateMap<BookingCreateInfo, Booking>();
        CreateMap<BookingUpdateInfo, Booking>();
    }
}
