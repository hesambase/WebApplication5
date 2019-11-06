using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class ServiceInfoWorker
    {
        public int Id { get; set; }
        [Required, MaxLength(80)]
        public string WName { get; set; }
        public string CName { get; set; }
        public int WId { get; set; }
        public int SId { get; set; }
        public string ServiceType { get; set; }
        public string WorkerNo { get; set; }
        [MaxLength(20)]
        public string Address { get; set; }
        public int ServiceTime { get; set; }
        public string Sign { get; set; }
        public DateTime SDate { get; set; }
    }
}
