using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;
using WebApplication4.Models;
using WebApplication4.ViewModel;
using WebApplication5.Data;

namespace WebApplication4.Services
{
    public class SqlUserData : IUserData
    {
        private ApplicationDbContext _context;

        public SqlUserData(ApplicationDbContext context)
        {
            _context = context;

        }
        public IEnumerable<User> GetAll()
        {
            return _context.Adminitrators.OrderBy(r => r.Username);
        }
        public User Validate(User user)
        {

            var userdata = _context.Adminitrators.FirstOrDefault(
       x => x.Username == user.Username && x.Password==user.Password);
    
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
