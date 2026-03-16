using BlazorJWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace BlazorJWT.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
[IgnoreAntiforgeryToken] // Allow DataManager POSTs when using JWT bearer authentication.
public class GridController : ControllerBase
{

  [HttpPost]
  public IActionResult Post([FromBody] DataManagerRequest dm)
  {
    var data = OrdersDetails.GetAllRecords().AsQueryable();
    var total = data.Count();

    return Ok(new { result = data.ToList(), count = total });
  }
}
