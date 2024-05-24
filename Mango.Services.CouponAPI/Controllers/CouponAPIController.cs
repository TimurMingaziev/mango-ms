using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponAPIController
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CouponAPIController(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
                Result = _mapper.Map<CouponDto>(coupon),
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
    [Route("GetByCode/{code}")]
    public ResponseDto Get(string code)
    {
        try
        {
            var coupon = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
            var response = new ResponseDto()
            {
                Result = _mapper.Map<CouponDto>(coupon),
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

    [HttpPost]
    public ResponseDto Post([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _dbContext.Coupons.Add(coupon);
            _dbContext.SaveChanges();

            var response = new ResponseDto()
            {
                Result = _mapper.Map<CouponDto>(coupon),
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

    [HttpPut]
    public ResponseDto Put([FromBody] CouponDto couponDto)
    {
        try
        {
            Coupon coupon = _mapper.Map<Coupon>(couponDto);
            _dbContext.Coupons.Update(coupon);
            _dbContext.SaveChanges();

            var response = new ResponseDto()
            {
                Result = _mapper.Map<CouponDto>(coupon),
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

    [HttpDelete]
    public ResponseDto Put(int id)
    {
        try
        {
            Coupon coupon = _dbContext.Coupons.Find(id);
            if (coupon != null)
            {
                _dbContext.Coupons.Remove(coupon);
                _dbContext.SaveChanges();
            }

            return new ResponseDto();
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