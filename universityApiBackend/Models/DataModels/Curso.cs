using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models.DataModels
{
    public class Curso : BaseEntity
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, StringLength(280)]
        public string ShortDescription { get; set; }

        [Required, StringLength(500)]
        public string LongDescription { get; set; }

        public string TargetAudiences { get; set; }

        public string Goals { get; set; }

        public string Requirements { get; set; }

        public enum Level { Basic, Intermediate, Advanced } 
    }
}
