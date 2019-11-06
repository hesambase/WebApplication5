using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class SqlServiceInfoData:IServiceInfoData
    {
        private ApplicationDbContext _context;

        public SqlServiceInfoData(ApplicationDbContext context)
        {
            _context = context;

        }
        public ServiceInfoWorker Get(int id)
        {
            return _context.ServiceInfo.LastOrDefault(r => r.WId == id);
        }
       
    }
}
