using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data.Entities;

namespace SchoolDiary.Domain.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=PC726;Database=helloappdb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new Role { Id = 1, Name = "admin" };
            var userRole = new Role { Id = 2, Name = "user" };
            var adminUser = new User
            {
                Id = 1,
                Login = "42ama",
                Password = "123456",
                RoleId = adminRole.Id
            };
            var defaultUser = new User
            {
                Id = 2,
                Login = "user",
                Password = "123456",
                RoleId = userRole.Id
            };
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, defaultUser });
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            base.OnModelCreating(modelBuilder);
        }
    }
}
