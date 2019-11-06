using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface ICustomerData
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
        Customer Add(Customer customer);
        Customer Delete(int id);
        Customer Update(int id, Customer customer);
    }
}
