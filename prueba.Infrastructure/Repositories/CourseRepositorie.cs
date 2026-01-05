using Microsoft.EntityFrameworkCore;
using prueba.Domain.Entities;
using prueba.Domain.Enums;
using prueba.Domain.Interface;
using prueba.Infrastructure.Data;

namespace prueba.Infrastructure.Repositories;

public class CourseRepositorie : ICourseRepositorie
{
    private readonly AppDbContext _dbContext;
    public CourseRepositorie(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Course>> GetAllCourses()
    {
        return await _dbContext.Courses
            .Where(c => !c.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetPagedCourses(CourseStatus? status, int page, int pageSize)
    {
        var query = _dbContext.Courses
            .Where(c => !c.IsDeleted)
            .AsQueryable();
        
        if(status.HasValue)
        {
                query = query.Where(c => c.Status == status.Value);
        }

        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task AddCourseAsync(Course course)
    {
        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course)
    {
        _dbContext.Courses.Update(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _dbContext.Courses
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
            throw new KeyNotFoundException($"Curso con id {id} no encontrado");

        course.IsDeleted = true;
        course.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
    }

    public async Task PublishCourseAsync(int id)
    {
        var course = await _dbContext.Courses.FindAsync(id);
        if (course == null) throw new KeyNotFoundException();
        
        course.Status = CourseStatus.Published;
        course.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public async Task UnpublishCourseAsync(int id)
    {
        var course = await _dbContext.Courses.FindAsync(id);
        if (course == null) throw new KeyNotFoundException();
        
        course.Status = CourseStatus.Draft;
        course.UpdatedAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _dbContext.Courses
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Course>?> SearchCoursesAsync(string? q, CourseStatus? status, int page, int pageSize)
    {
        var query = _dbContext.Courses
            .Where(c => c.IsDeleted)
            .AsQueryable();//Convierte una coleccion en una consulta dinamica

        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(c => c.Status == status.Value);
        
        return await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize) 
            .ToListAsync();
    }
}