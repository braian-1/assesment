using prueba.Domain.Entities;
using prueba.Domain.Enums;
using prueba.Domain.Interface;

namespace prueba.Application.Services;

public class CourseService
{
    private readonly ICourseRepositorie _repositorie;

    public CourseService(ICourseRepositorie repositorie)
    {
        _repositorie = repositorie;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _repositorie.GetAllCourses();
    }
    
    public async Task<IEnumerable<Course>> GetPagedCoursesAsync(
        CourseStatus? status,
        int page,
        int pageSize)
        => await _repositorie.GetPagedCourses(status, page, pageSize);


    public async Task AddCourseAsync(Course course)
    {
        await _repositorie.AddCourseAsync(course);
    }

    public async Task UpdateCourseAsync(Course course)
    {
        await _repositorie.UpdateCourseAsync(course);
    }

    public async Task DeleteCourseAsync(int id)
    {
        await _repositorie.DeleteCourseAsync(id);
    }
    
    public Task PublishCourseAsync(int id)
    => _repositorie.PublishCourseAsync(id);
    
    public Task UnpublishCourseAsync(int id)
    => _repositorie.UnpublishCourseAsync(id);
    
    public Task<Course?> GetCourseAsync(int id)
    => _repositorie.GetByIdAsync(id);
    
    public Task<IEnumerable<Course>> Search(
        string? q,
        CourseStatus? status,
        int page,
        int pageSize)
    =>_repositorie.SearchCoursesAsync(q, status, page, pageSize);
}