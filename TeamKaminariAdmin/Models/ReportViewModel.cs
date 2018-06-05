using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamKaminariAdmin.Models
{
    public class ReportViewModel
    {
        [Key]
        public Guid ReportKey { get; set; }
        public List<Customer> Customers { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Session> Sessions { get; set; }
     }
}