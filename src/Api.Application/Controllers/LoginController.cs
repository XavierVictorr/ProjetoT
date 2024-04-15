using System.Net;
using Domain.Dtos;
using Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace application.Controllers;

[Route("Api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private IloginService _service;
    
    public LoginController(IloginService service)
    {
        _service = service;
    }
    
    [HttpPost]
    public async Task<object> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (loginDto == null)
        {
            return BadRequest();
        }

        try
        {
            var result = await _service.FindByLogin(loginDto);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message); 
        }
    }
}