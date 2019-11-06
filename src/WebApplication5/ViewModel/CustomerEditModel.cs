using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.ViewModel
{
    public class CustomerEditModel
    {
        public int Id { get; set; }
        [Display(Name = "Customer Name")]
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string CustNo { get; set; }
        [MaxLength(20)]
        public string Tel { get; set; }
        [MaxLength(11)]
        public string Mobile { get; set; }

        public DateTime RegisterDate { get; set; }
        public string Email { get; set; }
    }
}
