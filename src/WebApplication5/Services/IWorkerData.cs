using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public interface IWorkerData
    {
       IEnumerable<Worker> GetAll();
        Worker Get(int id);
        Worker Add(Worker worker);
        Worker Delete(int id);
        Worker Update(int id,Worker worker);
        Worker Validate(Worker worker);
       // Worker ServiceInformation(int id);


    }
}
