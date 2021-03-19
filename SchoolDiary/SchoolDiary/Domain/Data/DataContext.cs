using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Helpers;

namespace SchoolDiary.Domain.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new Role { Id = 1, Name = "admin" };
            var userRole = new Role { Id = 2, Name = "user" };
            var teacherRole = new Role { Id = 3, Name = "teacher" };
            modelBuilder.Entity<Class>().HasData(new Class[]
            {
                new Class
                {
                    Id = 1,
                    Name = "7Г"
                },
                new Class
                {
                    Id = 2,
                    Name = "8Б"
                }
            });
            using (PasswordHasher ph = new PasswordHasher())
            {
                var adminUser = new User
                {
                    Id = 1,
                    Login = "42ama",
                    Password = ph.GenerateHash("qwerty"),
                    RoleId = adminRole.Id,
                    Firstname = "Максим",
                    Lastname = "Алонов",
                    Patronymic = "Александрович",
                    Phone = "+7-927-228-14-88"
                };
                var defaultUser = new User
                {
                    Id = 2,
                    Login = "grsann",
                    Password = ph.GenerateHash("123456"),
                    RoleId = userRole.Id,
                    Firstname = "Анна",
                    Lastname = "Герасимова",
                    Patronymic = "Сергеевна",
                    Phone = "+7-927-777-77-77"
                };
                modelBuilder.Entity<User>().HasData(new User[] { adminUser, defaultUser });
                modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, teacherRole });
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
