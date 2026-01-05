using Microsoft.AspNetCore.Mvc;
using prueba.Application.Services;
using prueba.Domain.Entities;

namespace prueba.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController :  ControllerBase
{
    private readonly UserService _userService;
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsers();
        return Ok(user);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        await _userService.AddUser(user);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        user.Id = id;
        await _userService.UpdateUser(user);
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
        return Ok();
    }
}