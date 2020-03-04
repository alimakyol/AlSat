using AlSat.Data.DAL;
using AlSat.Server.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlSat.Server.Controllers
{
  public class ErrorController : BaseController
  {
    [AllowAnonymous]
    [Route("/error")]
    public IActionResult Error()
    {
      BaseResponse response = new BaseResponse
      {
        Status = Enums.ResponseStatus.Error,
        Message = "An error occured."
      };

      return Ok(response);
    }
  }
}