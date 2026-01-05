using prueba.Domain.Enums;

namespace prueba.Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } =  string.Empty;
    public CourseStatus Status { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    
}