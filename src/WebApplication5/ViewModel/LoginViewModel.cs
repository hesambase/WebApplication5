using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WebApplication4.Models;

namespace WebApplication4.ViewModel
{
    public class LoginViewModel
    {
        public IEnumerable<User> Administrators { get; set; }
    }
}