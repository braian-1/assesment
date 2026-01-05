using prueba.Domain.Entities;
using prueba.Domain.Interface;

namespace prueba.Application.Services;

public class LessonService
{
    private readonly ILessonRepositorie _repositorie;

    public LessonService(ILessonRepositorie repositorie)
    {
        _repositorie = repositorie;
    }

    public async Task<IEnumerable<Lesson>> GetAllLessonsAsync()
    {
        return await _repositorie.GetLessonsAsync();
    }

    public async Task AddLessonAsync(Lesson lesson)
    {
        await _repositorie.AddLessonAsync(lesson);
    }

    public async Task UpdateLessonAsync(Lesson lesson)
    {
        await _repositorie.UpdateLessonAsync(lesson);
    }

    public async Task DeleteLessonAsync(int id)
    {
        await _repositorie.DeleteLessonAsync(id);
    }
}