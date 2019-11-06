using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Display(Name="Worker Name")]
        [Required,MaxLength(80)]
        public string Name { get; set; }
        public ServiceType Service { get; set; }
        public string WorkerNo { get; set; }
        [MaxLength(20)]
        public string Tel { get; set; }
        [MaxLength(11)]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
        
        public DateTime RegisterDate { get; set; }
        public string Password { get; set; }
    }
}
