using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Api_Backend.Models.DataModels
{
    public class Chapter:BaseEntity
    {

        


        public int CourseId { get; set; }
        //public virtual Course Course { get; set; } = new Course();

        

        [Required]
        public string List = string.Empty;
    }
}
