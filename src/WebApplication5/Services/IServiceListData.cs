using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;


namespace WebApplication4.Services
{
    public interface IServiceListData
    {
        IEnumerable<ServiceList> GetAll();
        ServiceList Get(int id);
        ServiceList Add(ServiceList servicelist);
        ServiceList Delete(int id);
        ServiceList Update(int id, ServiceList servicelist);
        ServiceList GetTask(int workerId);
    }
}
