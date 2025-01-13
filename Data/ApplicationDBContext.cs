using Microsoft.EntityFrameworkCore;
namespace SchoolProject.Data

{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> opt) :
          base(opt)
        {
        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Schedual> Scheduals { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
       

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Student>()
				.HasOne(s => s.StudentParent)
				.WithMany(p =>p.ParentStudent)
				.HasForeignKey(s => s.ParentId)
				.OnDelete(DeleteBehavior.Restrict);
		}


	}
}
