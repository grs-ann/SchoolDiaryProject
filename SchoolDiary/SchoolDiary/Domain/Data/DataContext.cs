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
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TeachersSubjects> TeachersSubjects { get; set; }
        public DbSet<TeachersClasses> TeachersClasses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<LessonTime> LessonTimes { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
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
                    Phone = "+7-927-777-77-77",
                };
                var teacherUser = new User
                {
                    Id = 3,
                    Login = "pasha",
                    Password = ph.GenerateHash("123456"),
                    RoleId = teacherRole.Id,
                    Firstname = "Павел",
                    Lastname = "Панфилов",
                    Patronymic = "Вячеславович",
                    Phone = "+7-927-748-99-99"
                };
                modelBuilder.Entity<User>().HasData(new User[] { adminUser, defaultUser, teacherUser });
                modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, teacherRole });
            }
            modelBuilder.Entity<Subject>().HasData(new Subject[]
            {
                new Subject
                {
                    Id = 1,
                    Title = "Алгебра"
                },
                new Subject
                {
                    Id = 2,
                    Title = "Геометрия"
                }, 
                new Subject
                {
                    Id = 3,
                    Title = "Физика"
                },
                new Subject
                {
                    Id = 4,
                    Title = "Русский язык"
                }
            });
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 1, Salary = 228000, UserId = 3 });
            modelBuilder.Entity<Time>().HasData(
                new Time[]
                {
                    new Time
                    {
                        Id = 1,
                        LessonTime = "8:00-8:45"
                    },
                    new Time
                    {
                        Id = 2,
                        LessonTime = "8:55-9:40"
                    },
                    new Time
                    {
                        Id = 3,
                        LessonTime = "10:00-10:45"
                    },
                    new Time
                    {
                        Id = 4,
                        LessonTime = "11:05-11:50"
                    },
                    new Time
                    {
                        Id = 5,
                        LessonTime = "12:00-12:45"
                    },
                    new Time
                    {
                        Id = 6,
                        LessonTime = "12:55-13:40"
                    },
                    new Time
                    {
                        Id = 7,
                        LessonTime = "13:50-14:35"
                    },
                    new Time
                    {
                        Id = 8,
                        LessonTime = "14:45-15:30"
                    },
                });
            modelBuilder.Entity<Day>().HasData(new Day[]
                    {
                        new Day
                        {
                            Id = 1,
                            Name = "Понедельник"
                        },
                        new Day
                        {
                            Id = 2,
                            Name = "Вторник"
                        },
                        new Day
                        {
                            Id = 3,
                            Name = "Среда"
                        },
                        new Day
                        {
                            Id = 4,
                            Name = "Четверг"
                        },
                        new Day
                        {
                            Id = 5,
                            Name = "Пятница"
                        },
                        new Day
                        {
                            Id = 6,
                            Name = "Суббота"
                        },
                        new Day
                        {
                            Id = 7,
                            Name = "Воскресенье"
                        },
                    });

           base.OnModelCreating(modelBuilder);
        }
    }
}
