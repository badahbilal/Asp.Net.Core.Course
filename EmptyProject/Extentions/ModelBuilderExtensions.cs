using EmptyProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EmptyProject.Extentions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Bilal BADAH",
                    Email = "bilal@gmail.com",
                    ImagePath = "/images/01.png",
                    Department = Department.HR
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Ayman Badah",
                    Email = "bilal2@gmail.com",
                    ImagePath = "/images/02.jpg",
                    Department = Department.Networking
                },
                new Employee()
                {
                    Id = 3,
                    Name = "Ayoub Badah",
                    Email = "AyoubNa@gmail.com",
                    ImagePath = "/images/02.jpg",
                    Department = Department.DTT
                }
                );
        }
    }
}
