using prueba.Domain.Entities;
using prueba.Domain.Enums;

namespace prueba.Domain.Interface;

public interface ICourseRepositorie
{
    Task<IEnumerable<Course>> GetAllCourses();
    Task<IEnumerable<Course>> GetPagedCourses(CourseStatus? status,int page, int pageSize);
    Task AddCourseAsync(Course course);
    Task UpdateCourseAsync(Course course);
    Task DeleteCourseAsync(int id);
    
    Task PublishCourseAsync(int id);
    Task UnpublishCourseAsync(int id);
    Task<Course?> GetByIdAsync(int id);
    Task<IEnumerable<Course>?> SearchCoursesAsync(string? q,CourseStatus? status,int page,int pageSize);
}