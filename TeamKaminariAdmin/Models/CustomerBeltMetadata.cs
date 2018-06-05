using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TeamKaminariAdmin.Models
{
    public class CustomerBeltMetadata
    {
    }

    public partial class CustomerBelt
    {
        public List<Belt> Belts { get; set; }
        public CheckBoxField BeltAcheived { get; set; }
    }
}