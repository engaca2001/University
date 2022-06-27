using System.ComponentModel.DataAnnotations;

namespace University_Api_Backend.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
       // public  int UsuarioId   { get; set; }   
        //public Usuario Usuario { get; set; } = new Usuario();
        
        public  string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string DeletedBy { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;



    }
}
