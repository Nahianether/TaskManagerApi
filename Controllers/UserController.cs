using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Dtos;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using AutoMapper;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;

    public UsersController(IUserService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<UserReadDto>> GetUsers() =>
        await _service.GetAllUsersAsync() is { Count: > 0 } users
            ? Ok(_mapper.Map<List<UserReadDto>>(users))
            : NoContent();

    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadDto>> GetUser(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        return user == null ? NotFound() : _mapper.Map<UserReadDto>(user);
    }

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> CreateUser(UserCreateDto dto)
    {
        var user = _mapper.Map<User>(dto);
        var created = await _service.CreateUserAsync(user);
        var readDto = _mapper.Map<UserReadDto>(created);
        return CreatedAtAction(nameof(GetUser), new { id = readDto.Id }, readDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserUpdateDto dto)
    {
        var existing = await _service.GetUserByIdAsync(id);
        if (existing == null) return NotFound();

        _mapper.Map(dto, existing);
        await _service.UpdateUserAsync(id, existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id) =>
        await _service.DeleteUserAsync(id) ? NoContent() : NotFound();
}
