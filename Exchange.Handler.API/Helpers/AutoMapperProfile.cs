using AutoMapper;
using Exchange.Handler.API.Models;
using Exchange.Handler.Models;
using Exchange.Handler.Repository.Entities;

namespace Exchange.Handler.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GL, BuyingModel>()
                .ForMember(s => s.TradeId, opt => opt.MapFrom(t => t.TradeId))
                .ForMember(s => s.Quantity, opt => opt.MapFrom(t => t.Quantity))
                .ReverseMap();

            CreateMap<GL, SellingModel>()
                .ForMember(s => s.TradeId, opt => opt.MapFrom(t => t.TradeId))
                .ForMember(s => s.Quantity, opt => opt.MapFrom(t => t.Quantity))
                .ReverseMap();
        }
    }
}
