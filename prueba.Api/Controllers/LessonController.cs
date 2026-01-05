using Microsoft.AspNetCore.Mvc;
using prueba.Application.Services;
using prueba.Domain.Entities;

namespace prueba.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonController : ControllerBase
{
    private readonly LessonService _service;
    public LessonController(LessonService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var lessons = await _service.GetAllLessonsAsync();
        return Ok(lessons);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] Lesson lesson)
    {
        await _service.AddLessonAsync(lesson);
        return Ok(lesson);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] Lesson lesson)
    {
        await _service.UpdateLessonAsync(lesson);
        return Ok(lesson);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteLessonAsync(id);
        return Ok();
    }
}