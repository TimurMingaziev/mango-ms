using AutoMapper;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mapperConfig = new MapperConfiguration(option =>
        {
            option.CreateMap<CouponDto, Coupon>();
            option.CreateMap<Coupon, CouponDto>();
        });

        return mapperConfig;
    }
}