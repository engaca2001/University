using Microsoft.EntityFrameworkCore;
using University_Api_Backend.Models.DataModels;

namespace University_Api_Backend.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options){

        }

        // add DBsets (Tables of our database) 
          public DbSet<Usuario>? Usuarios { get; set; }
          public DbSet<Course>? Courses { get; set; }
          public DbSet<Category>? Categories { get; set; }
          public DbSet<Student>? Students { get; set; }
          public DbSet<Chapter>? Chapters { get; set; }

    }
}
