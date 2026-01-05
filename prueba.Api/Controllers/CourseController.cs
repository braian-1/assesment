using Microsoft.AspNetCore.Mvc;
using prueba.Application.Services;
using prueba.Domain.Entities;
using prueba.Domain.Enums;

namespace prueba.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly CourseService _service;
    public CourseController(CourseService service)
    {
        _service = service;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCourses(
        [FromQuery] string? q,
        [FromQuery] CourseStatus? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.Search(q, status, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> Summary(int id)
    {
        var course = await _service.GetCourseAsync(id);
        if(course == null) return NotFound();
        
        return Ok(new
        {
            course.Id,
            course.Title,
            course.Status,
            course.CreatedAt,
        });
    }

    [HttpPatch("{id}/publish")]
    public async Task<IActionResult> Publish(int id)
    {
        await  _service.PublishCourseAsync(id);
        return NoContent();
    }

    [HttpPatch("{id}/unpublish")]
    public async Task<IActionResult> Unpublish(int id)
    {
        await _service.UnpublishCourseAsync(id);
        return NoContent();
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPaged([FromQuery] CourseStatus? status, [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var courses = await _service.GetPagedCoursesAsync(status, page, pageSize);
        return Ok(courses);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _service.GetAllCoursesAsync();
        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourse([FromBody] Course course)
    {
        await _service.AddCourseAsync(course);
        return Ok(course);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCourse([FromBody] Course course)
    {
        await _service.UpdateCourseAsync(course);
        return Ok(course);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        await _service.DeleteCourseAsync(id);
        return Ok();
    }
}