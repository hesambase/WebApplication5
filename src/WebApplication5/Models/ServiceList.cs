using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Models
{
    public class ServiceList
    {
        public int Id { get; set; }
        public int CustID { get; set; }
        public int WorkerID { get; set; }
        public DateTime RegisterDate { get; set; }
        public int ServiceTime { get; set; }
        public float Cost { get; set; }

    }
}
