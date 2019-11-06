using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication4.Models;

namespace WebApplication2.ViewModel
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Worker> Workers { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<ServiceList> Service { get; set; }
        public string CurrentMessage { get; set; }
    }
}
