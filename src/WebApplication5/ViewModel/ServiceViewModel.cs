using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.ViewModel
{
    public class ServiceViewModel 
    {
        public IEnumerable<ServiceInfoWorker> ServiceInfo { get; set; }
    }
}
