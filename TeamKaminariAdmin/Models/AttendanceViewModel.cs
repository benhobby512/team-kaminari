using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamKaminariAdmin.Models
{
    public class AttendanceViewModel
    {
        public List<Customer> Customers { get; set; }
        public Guid SessionId { get; set; } 
    }
}