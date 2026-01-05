using System.Text.Json.Serialization;

namespace prueba.Domain.Entities;

public class Lesson
{
    public int Id { get; set; }
    
    public int CourseId { get; set; }
    
    [JsonIgnore]
    public Course? Course { get; set; }
    
    public string Title { get; set; } =  String.Empty;
    public int Order { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}