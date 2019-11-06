using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    //public class InMemoryWorkerData : IWorkerData
    //{

    //    public InMemoryWorkerData()
    //    {
    //        //_Worker = new List<Worker>
    //        //{
    //        //    new Worker {Id=1, Name="ali"  },
    //        //    new Worker {Id=2, Name="Hesam" },
    //        //    new Worker {Id=3, Name="Reza" },

    //        //};
    //    }
    //    public IEnumerable<Worker> GetAll()
    //    {
    //        return _Worker.OrderBy(r => r.Name);
    //    }

    //    public Worker Get(int id)
    //    {
    //        return _Worker.FirstOrDefault(r => r.Id == id);
    //    }

    //    public Worker Add(Worker worker)
    //    {
    //        worker.Id = _Worker.Max(r => r.Id) + 1;
    //        _Worker.Add(worker);
            
    //        return worker;
    //    }

    //    List<Worker> _Worker;
    //}
   
}
