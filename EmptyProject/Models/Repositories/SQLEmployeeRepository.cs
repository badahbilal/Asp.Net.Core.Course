using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EmptyProject.Models.Repositories
{
    public class SQLEmployeeRepository : ICompanyRepository<Employee>
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee entity)
        {

            context.Employees.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public Employee Delete(int id)
        {
            var employee = get(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public Employee get(int id)
        {
            var employee = context.Employees.SingleOrDefault(emp => emp.Id == id);
            return employee;
        }

        public IEnumerable<Employee> GetEntities()
        {
            return context.Employees;
        }

        public Employee Update(Employee entityChanged)
        {
            var employee = context.Employees.Attach(entityChanged);
            employee.State = EntityState.Modified;
            context.SaveChanges();
            return entityChanged;
        }
    }
}
