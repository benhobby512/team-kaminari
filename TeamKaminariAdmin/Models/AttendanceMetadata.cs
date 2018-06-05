using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamKaminariAdmin.Models
{
    public class AttendanceMetadata
    {

    }

    public partial class Attendance
    {
        [Display(Name = "Current Session")]
        public Session CurrentSession { get; set; }
        [Display(Name = "Sessions")]
        public IEnumerable<SelectListItem> Sessions { get; set; }
        public DateTime SessionSelected { get; set; }
    }
}