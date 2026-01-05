using Microsoft.EntityFrameworkCore;
using prueba.Domain.Entities;
using prueba.Domain.Interface;
using prueba.Infrastructure.Data;

namespace prueba.Infrastructure.Repositories;

public class LessonRepositorie : ILessonRepositorie
{
    private readonly AppDbContext _dbContext;
    public LessonRepositorie(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Lesson>> GetLessonsAsync()
    {
        return await _dbContext.Lessons.ToListAsync();
    }

    public async Task AddLessonAsync(Lesson lesson)
    {
        _dbContext.Lessons.Add(lesson);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateLessonAsync(Lesson lesson)
    {
        _dbContext.Lessons.Update(lesson);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteLessonAsync(int id)
    {
        var deleteLesson = await _dbContext.Lessons.FirstOrDefaultAsync(l => l.Id == id);
        _dbContext.Lessons.Remove(deleteLesson);
        await _dbContext.SaveChangesAsync();
    }
}