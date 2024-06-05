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
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(coupons);
    }

    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var response = await _couponService.CreateCouponAsync(model);

        if (response != null && response.Success)
        {
            return RedirectToAction(nameof(CouponIndex));
        }

        TempData["error"] = response?.Message;

        return View(model);
    }

    public async Task<IActionResult> CouponDelete(int couponId)
    {
        var response = await _couponService.DeleteCouponAsync(couponId);

        return RedirectToAction(nameof(CouponIndex));
    }
}