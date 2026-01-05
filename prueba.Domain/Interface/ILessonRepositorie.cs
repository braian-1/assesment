using prueba.Domain.Entities;

namespace prueba.Domain.Interface;

public interface ILessonRepositorie
{
    Task<IEnumerable<Lesson>> GetLessonsAsync();
    Task AddLessonAsync(Lesson lesson);
    Task UpdateLessonAsync(Lesson lesson);
    Task DeleteLessonAsync(int id);
}