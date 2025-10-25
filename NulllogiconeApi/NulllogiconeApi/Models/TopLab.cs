using System.ComponentModel.DataAnnotations;

namespace NulllogiconeApi.Models
{
    public class TopLab
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(500)]
        public string? Description { get; set; }
        
        [MaxLength(50)]
        public string? Category { get; set; }
        
        public int Priority { get; set; } = 1;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
    }
}