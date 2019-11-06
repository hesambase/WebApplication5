using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication4.Models;
using WebApplication5.Data;

namespace WebApplication4.Services
{
    public class SqlCustomerData : ICustomerData
    {
        private ApplicationDbContext _context;

        public SqlCustomerData(ApplicationDbContext context)
        {
            _context = context;

        }

        public Customer Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer;

        }

        public Customer Delete(int id)
        {
            var del = _context.Customers.First(r => r.Id == id);
            _context.Remove(del);
            _context.SaveChanges();
            return del;
        }

        public Customer Get(int id)
        {
            return _context.Customers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.OrderBy(r => r.Name);
        }

        public Customer Update(int id, Customer customer)
        {
            var entity = _context.Customers.FirstOrDefault(item => item.Id == id);

            // Validate entity is not null
            if (entity != null)
            {
                // Answer for question #2
                // Make changes on entity
                entity.Name = customer.Name;
                entity.Address = customer.Address;
                entity.CustNo = customer.CustNo;
                entity.Tel = customer.Tel;
                entity.Mobile = customer.Mobile;
                //worker.RegisterDate = System.DateTime.Now;
                entity.Email = customer.Email;


                // Update entity in DbSet
                _context.Customers.Update(entity);

                // Save changes in database
                _context.SaveChanges();
            }
            return entity;
        }
    }
}
