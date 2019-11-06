using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication4.Models;
using System.Data.SqlClient;

namespace WebApplication4.Services
{
    public class SqlServiceListData : IServiceListData
    {
        private ApplicationDbContext _context;

        public SqlServiceListData(ApplicationDbContext context)
        {
            _context = context;
        }
        public ServiceList Add(ServiceList servicelist)
        {
            _context.Service.Add(servicelist);

            _context.SaveChanges();

            string cname = null;
            string address = null;
            string wname = null;
            string workerno = null;
            int wid = 0;

            string servicetype = null;
            int servicetime = 0;
            int sid = 0;
            string cn = configuration.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
           // SqlConnection cn = new SqlConnection("Data Source = (local); Integrated Security = False; Database = clnsyste_aspnet; User ID = clnsyste_Hesam; Connect Timeout = 15; Encrypt = False; Packet Size = 4096");

            cn.Open();
            SqlCommand cm3 = new SqlCommand("select  Name,Address from Customers Where Id=" + servicelist.CustID , cn);
            SqlDataReader reader3 = cm3.ExecuteReader();
            while (reader3.Read())
            {
                cname = (string)reader3["Name"];
                address = (string)reader3["Address"];
            }
            reader3.Dispose();
            cn.Close();
            cn.Open();
            SqlCommand cm4 = new SqlCommand("select  Id,Name,WorkerNo,Case"+ 
                " When Service=0 THEN 'Nezafat'"+
                " WHEN Service=1 THEN 'Mehmandari'"+
                " WHEN Service=2 THEN 'Ghalishooyee'"+
                " WHEN Service=3 THEN 'Naghashi' End AS ServiceType"+
                " from Workers Where Id=" + servicelist.WorkerID, cn);
            SqlDataReader reader4 = cm4.ExecuteReader();
            while (reader4.Read())
            {
                wid = (int)reader4["Id"];
                wname = (string)reader4["Name"];
                workerno = (string)reader4["WorkerNo"];
                servicetype = (string)reader4["ServiceType"];
            }
            reader4.Dispose();
            cn.Close();
            cn.Open();
            SqlCommand cm5 = new SqlCommand("INSERT INTO ServiceInfo(CName,WName,ServiceType,WorkerNo,Address,ServiceTime,Sign,WId,SId)"+
                " Values(N'"+ cname+"',N'"+wname+"',N'"+servicetype+"',N'"+workerno+"',N'"+address+"',"+servicelist.ServiceTime+",'',"+wid+","+servicelist.Id+")", cn);

            cm5.ExecuteNonQuery();
            cn.Close();

            return servicelist;
        }

        public ServiceList Delete(int id)
        {
            var del = _context.Service.First(r => r.Id == id);
            _context.Remove(del);
            _context.SaveChanges();
            return del;
        }

        public ServiceList Get(int id)
        {
            return _context.Service.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<ServiceList> GetAll()
        {
            return _context.Service.OrderBy(r => r.RegisterDate);
        }

        public ServiceList Update(int id, ServiceList servicelist)
        {
            var entity = _context.Service.FirstOrDefault(item => item.Id == id);
            // Validate entity is not null
            if (entity != null)
            {
                // Answer for question #2
                // Make changes on entity
                entity.CustID = servicelist.CustID;
                entity.WorkerID = servicelist.WorkerID;
                entity.ServiceTime = servicelist.ServiceTime;
                entity.Cost = servicelist.Cost;
                


                // Update entity in DbSet
                _context.Service.Update(entity);

                // Save changes in database
                _context.SaveChanges();
            }
            return entity;
        }
        public ServiceList GetTask(int workerId)
        {
            return _context.Service.FirstOrDefault(r => r.WorkerID == workerId);
        }

    }
}
