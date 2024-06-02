using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Mango.Web.Utility;

namespace Mango.Web.Services;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> GetCouponAsync(string code)
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Get,
            Url = ServicesUrls.CouponAPI + $"/api/coupon/GetByCode/{code}",
        });

        return result;
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Get,
            Url = ServicesUrls.CouponAPI + "/api/coupon",
        });

        return result;
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Get,
            Url = ServicesUrls.CouponAPI + $"/api/coupon/{id}",
        });

        return result;
    }

    public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Post,
            Url = ServicesUrls.CouponAPI + $"/api/coupon/",
            Data = couponDto,
        });

        return result;
    }

    public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Put,
            Url = ServicesUrls.CouponAPI + $"/api/coupon/",
            Data = couponDto,
        });

        return result;
    }

    public async Task<ResponseDto?> DeleteCouponAsync(int id)
    {
        var result = await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiType.Delete,
            Url = ServicesUrls.CouponAPI + $"/api/coupon?id={id}",
        });

        return result;
    }
}