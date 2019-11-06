using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication2.Models;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;


namespace WebApplication2.Services
{
    public class SqlWorkerData : IWorkerData
    {
        private ApplicationDbContext _context;

        public SqlWorkerData(ApplicationDbContext context)
        {
            _context = context;

        }
        public Worker Add(Worker worker)
        {
            _context.Workers.Add(worker);
            _context.SaveChanges();
            return worker;
        }

        public Worker Get(int id)
        {
            return _context.Workers.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Worker> GetAll()
        {
            return _context.Workers.OrderBy(r => r.Name);
        }

        public Worker Delete(int id)
        {

            var del = _context.Workers.First(r => r.Id == id);
            _context.Remove(del);
            _context.SaveChanges();
            return del;
        }

        public Worker Update(int id, Worker worker)
        {
            var entity = _context.Workers.FirstOrDefault(item => item.Id == id);

            // Validate entity is not null
            if (entity != null)
            {
                // Answer for question #2
                // Make changes on entity
                entity.Name = worker.Name;
                entity.Service = worker.Service;
                entity.WorkerNo = worker.WorkerNo;
                entity.Tel = worker.Tel;
                entity.Mobile = worker.Mobile;
                entity.Email = worker.Email;
                //worker.RegisterDate = System.DateTime.Now;
                entity.Password = worker.Password;


                // Update entity in DbSet
                _context.Workers.Update(entity);

                // Save changes in database
                _context.SaveChanges();
            }
            return entity;
        }

        public Worker Validate(Worker worker)
        {

            var userdata = _context.Workers.FirstOrDefault(
       x => x.Mobile == worker.Mobile && x.Password == worker.Password);

            //var usr = _context.Adminitrators.Where(r => r.Username.Equals(username));
            //if (!usr.Equals(null))
            //{
            //    var psw = _context.Adminitrators.Where(r => r.Password.Equals(password));
            //    if (!psw.Equals(null))
            //    {

            //    }

            //}
            return userdata;
        }


      

    }
}
