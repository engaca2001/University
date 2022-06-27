using System.ComponentModel.DataAnnotations;

namespace University_Api_Backend.Models.DataModels
{
    public class Usuario
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required,EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

       // public  BaseEntity BaseEntity { get; set; } = new BaseEntity();

       //[Required]
        //public Category Category { get; set; } = new Category();

        //[Required]
        //public Chapter Chapter { get; set; } = new Chapter();

        //[Required]
        //public Course Course { get; set; } = new Course();

        //[Required]
        //public Student Student { get; set; } = new Student();

        

    }
}
