using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamKaminariAdmin.Models
{
    public class BeltMetadata
    {
        [Required, Display(Name = "Belt Colour")]
        public string BeltColour { get; set; }
    }

    public partial class Belt
    {
        public bool BeltAchieved { get; set; }
    }
}