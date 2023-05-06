using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public enum Level { Basic, Medium, Advanced, Expert }

    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(280)]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }

        public string TargetAudiences { get; set; }

        public string Goals { get; set; }

        public string Requirements { get; set; }

        public Level Level { get; set; } = Level.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }

}
