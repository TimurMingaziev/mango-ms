using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }

    public async Task<IActionResult> CouponIndex()
    {
        List<CouponDto> coupons = [];

        var response = await _couponService.GetAllCouponsAsync();

        if (response != null && response.Success)
        {
            coupons = JsonConvert.DeserializeObject<List<CouponDto>>(response.Result.ToString());
        }
        return View(coupons);
    }

    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }
}