using System.Collections.Generic;
using System.Linq;

namespace EmptyProject.Models.Repositories
{
    public class EmployeeRepository : ICompanyRepository<Employee>
    {
        List<Employee> employees;
        public EmployeeRepository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee() { Id = 1, Name = "Bilal", Email = "bilal@gmail.com", ImagePath = "/images/01.png", Department = Department.HR });
            employees.Add(new Employee() { Id = 2, Name = "Bilal2", Email = "bilal2@gmail.com", ImagePath = "/images/02.jpg", Department = Department.Networking });
            employees.Add(new Employee() { Id = 3, Name = "Bilal3", Email = "bilal3@gmail.com", ImagePath = "/images/03.jpeg", Department = Department.DTT });
            employees.Add(new Employee() { Id = 4, Name = "Bilal4", Email = "bilal4@gmail.com", ImagePath = "/images/04.jpg", Department = Department.IT });

        }

        public Employee Add(Employee employee)
        {
            employee.Id = employees.Max(employee => employee.Id) + 1;
            employee.ImagePath = "/images/NoImage.png";
            employees.Add(employee);
            return employee;
        }



        public Employee get(int id)
        {
            return employees.FirstOrDefault(em => em.Id == id);
        }

        public IEnumerable<Employee> GetEntities()
        {
            return employees;
        }

        public Employee Update(Employee entityChanged)
        {
            var employee = employees.FirstOrDefault(emp => emp.Id == entityChanged.Id);
            if (employee != null)
            {
                employee.Name = entityChanged.Name;
                employee.Department = entityChanged.Department;
                employee.Email = entityChanged.Email;
                employee.ImagePath = entityChanged.ImagePath;
            }

            return employee;
        }


        public Employee Delete(int id)
        {
            var employee = employees.FirstOrDefault(emp => emp.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
            }

            return employee;
        }
    }
}
/*
             employees.Add(new Employee() { Id = 1, Name = "Bilal" , Email = "bilal@gmail.com" , Image = "/images/01.png"});
            employees.Add(new Employee() { Id = 2, Name = "Bilal2", Email = "bilal2@gmail.com", Image = "/images/02.jpg" });
            employees.Add(new Employee() { Id = 3, Name = "Bilal3", Email = "bilal3@gmail.com", Image = "/images/03.jpeg" });
            employees.Add(new Employee() { Id = 4, Name = "Bilal4" , Email = "bilal4@gmail.com", Image = "/images/04.jpg" });

            employees.Add(new Employee() { Id = 1, Name = "Bilal"  , Email = "bilal@gmail.com" , Image = "/images/02.jpg"});
            employees.Add(new Employee() { Id = 2, Name = "Bilal2", Email = "bilal2@gmail.com", Image = "/images/02.jpg" });
            employees.Add(new Employee() { Id = 3, Name = "Bilal3", Email = "bilal3@gmail.com", Image = "/images/02.jpg" });
            employees.Add(new Employee() { Id = 4, Name = "Bilal4" , Email = "bilal4@gmail.com", Image = "/images/02.jpg" });
 */
