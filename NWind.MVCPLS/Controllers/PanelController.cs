using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NWind.MVCPLS.Controllers;

public class PanelController : Controller
{
    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Admin()
    {
        return View();
    }
}