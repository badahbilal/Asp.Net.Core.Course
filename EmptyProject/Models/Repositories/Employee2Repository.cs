using System.Collections.Generic;
using System.Linq;

namespace EmptyProject.Models.Repositories
{
    public class Employee2Repository : ICompanyRepository<Employee>
    {
        List<Employee> employees;
        public Employee2Repository()
        {
            employees = new List<Employee>();
            employees.Add(new Employee() { Id = 1, Name = "BADAH " });
            employees.Add(new Employee() { Id = 2, Name = "BADAH 2" });
            employees.Add(new Employee() { Id = 3, Name = "BADAH 3" });
            employees.Add(new Employee() { Id = 4, Name = "BADAH 4" });
        }

        public Employee Add(Employee entity)
        {
            return null;
        }

        public Employee Delete(int id)
        {
            return null;
        }

        public Employee get(int id)
        {
            return employees.FirstOrDefault(em => em.Id == id);
        }

        public Employee Update(Employee entityChanged)
        {
            return null;
        }



        IEnumerable<Employee> ICompanyRepository<Employee>.GetEntities()
        {
            return null;
        }
    }
}
