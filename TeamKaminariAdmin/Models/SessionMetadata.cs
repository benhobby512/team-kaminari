using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamKaminariAdmin.Models
{
    public class SessionMetadata
    {
        [Display(Name = "Session Number")]
        public int? SessionNo { get; set; }
    }

    public partial class Session
    {
        public DateTime SessionDate { get; set; }
    }
}