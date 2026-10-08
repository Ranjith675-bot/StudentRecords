using System.ComponentModel.DataAnnotations;

namespace StudentRecordsApi.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public int RollNumber { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Course { get; set; } = string.Empty;

        [Range(0, 100)]
        public int Mark1 { get; set; }

        [Range(0, 100)]
        public int Mark2 { get; set; }

        [Range(0, 100)]
        public int Mark3 { get; set; }
    }
}
