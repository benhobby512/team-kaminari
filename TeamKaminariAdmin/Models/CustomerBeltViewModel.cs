using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TeamKaminariAdmin.Models
{
    public class CustomerBeltViewModel
    {
        public List<CustomerBelt> CustomerBelts { get; set; }
        public List<Belt> Belts { get; set; }

        public Guid? CustomerId { get; set; }
    }
}