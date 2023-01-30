using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;

namespace Discount.Grpc.MappingProfile;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CopounModel>().ReverseMap();
    }
}