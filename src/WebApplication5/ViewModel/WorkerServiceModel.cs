using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.ViewModel
{
    public class WorkerServiceModel
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
