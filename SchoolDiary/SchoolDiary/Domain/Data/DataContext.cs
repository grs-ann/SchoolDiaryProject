using Microsoft.EntityFrameworkCore;
using SchoolDiary.Domain.Data.Entities;
using SchoolDiary.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Mark> Marks { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var adminRole = new Role { Id = 1, Name = "admin" };
            var studentRole = new Role { Id = 2, Name = "student" };
            var teacherRole = new Role { Id = 3, Name = "teacher" };
            var studentClass = new Class { Id = 1, Name = "7Г" };
            modelBuilder.Entity<Class>().HasData(new Class[]
            {
                studentClass,
                new Class
                {
                    Id = 2,
                    Name = "8Б"
                }
            });
            var marks = new List<Mark>()
            {
                new Mark
                {
                    Id = 1,
                    Name = "5"
                },
                new Mark
                {
                    Id = 2,
                    Name = "4"
                },
                new Mark
                {
                    Id = 3,
                    Name = "3"
                },
                new Mark
                {
                    Id = 4,
                    Name = "2"
                },
                new Mark
                {
                    Id = 5,
                    Name = "ОТ"
                },
                new Mark
                {
                    Id = 6,
                    Name = "УП"
                }
            };
            modelBuilder.Entity<Mark>().HasData(marks);

            var subjects = new List<Subject>()
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
            };
            modelBuilder.Entity<Subject>().HasData(subjects);
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
                    RoleId = studentRole.Id,
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
                modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, studentRole, teacherRole });

                modelBuilder.Entity<Student>().HasData(new Student { Id = 1, UserId = defaultUser.Id, ClassId = studentClass.Id });
            }
            
            modelBuilder.Entity<Teacher>().HasData(new Teacher { Id = 1, Salary = 228000, UserId = 3 });


            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Classes)
                .WithMany(c => c.Teachers)
                .UsingEntity(j => j.ToTable("TeachersAndClasses"));

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithMany(c => c.Teachers)
                .UsingEntity(j => j.ToTable("TeachersAndSubjects"));

            // Many to many relation for 'Mark' and 'Student' entities. 
            modelBuilder.Entity<Mark>().HasMany(m => m.Students).WithMany(s => s.Marks)
                .UsingEntity<Grade>(g => g
                        .HasOne(pt => pt.Student)
                        .WithMany(t => t.Grades)
                        .HasForeignKey(pt => pt.StudentId),
                        g => g
                            .HasOne(pt => pt.Mark)
                            .WithMany(p => p.Grades)
                            .HasForeignKey(pt => pt.MarkId),
                        g =>
                        {
                            g.Property(pt => pt.GradeDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                            g.HasKey(pt => pt.Id);
                            g.ToTable("Grades");
                        });
            base.OnModelCreating(modelBuilder);
        }
    }
}
