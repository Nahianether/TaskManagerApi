using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Dtos;
using TaskManagerApi.Models;
using TaskManagerApi.Services;
using AutoMapper;

namespace TaskManagerApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;
    private readonly IMapper _mapper;

    public TasksController(ITaskService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetTasks()
    {
        var tasks = await _service.GetAllTasksAsync();
        return Ok(_mapper.Map<IEnumerable<TaskReadDto>>(tasks));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskReadDto>> GetTaskById(int id)
    {
        var task = await _service.GetTaskByIdAsync(id);
        return task == null ? NotFound() : _mapper.Map<TaskReadDto>(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        var created = await _service.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = created.Id }, _mapper.Map<TaskReadDto>(created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskUpdateDto dto)
    {
        var existing = await _service.GetTaskByIdAsync(id);
        if (existing == null) return NotFound();

        _mapper.Map(dto, existing);
        await _service.UpdateTaskAsync(id, existing);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id) =>
        await _service.DeleteTaskAsync(id) ? NoContent() : NotFound();
}
