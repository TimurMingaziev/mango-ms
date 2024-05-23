using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponAPIController
{
    private readonly ApplicationDbContext _dbContext;

    public CouponAPIController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ResponseDto Get()
    {
        try
        {
            var coupons = _dbContext.Coupons.ToList();
            var response = new ResponseDto()
            {
                Result = coupons,
            };
            return response;
        }
        catch (Exception ex)
        {
            var response = new ResponseDto()
            {
                Success = false,
                Message = ex.Message
            };
            return response;
        }
    }

    [HttpGet]
    [Route("{id:int}")]
    public ResponseDto Get(int id)
    {
        try
        {
            var coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
            var response = new ResponseDto()
            {
                Result = coupon,
            };
            return response;
        }
        catch (Exception ex)
        {
            var response = new ResponseDto()
            {
                Success = false,
                Message = ex.Message
            };
            return response;
        }
    }
}