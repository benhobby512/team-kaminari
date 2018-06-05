using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TeamKaminariAdmin.Models
{
    public class CustomerMetadata
    {
        [EmailAddress, Display(Name = "Email Address")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string EmailAddress { get; set; }
        [Phone]
        public string Tel1 { get; set; }
        [Phone]
        public string Tel2 { get; set; }
        [Display(Name = "Medical Info")]
        public string MedicalInfo { get; set; }
        [Display(Name = "License Number")]
        public string LicenseNo { get; set; }
        [Display(Name = "Liability Waiver")]
        public bool LiabilityWaiver { get; set; }
        [Display(Name = "Photo Release")]
        public bool PhotoRelease { get; set; }
        [Display(Name = "Paying Monthly")]
        public bool PayingMonthly { get; set; }
        [Display(Name = "License Paid")]
        public bool LicensePaid { get; set; }
        [DataType(DataType.DateTime), Display(Name = "License Renewal Date")]
        public DateTime? LicenseResetDate { get; set; }
    }
    public partial class Customer
    {
        public IEnumerable<SelectListItem> BeltEnumerable { get; set; }
        public string BeltSelected { get; set; }
        public EmergencyContact EmergencyContact { get; set; }
        public List<EmergencyContact> EmergencyContactList { get; set; }
        public bool CustomerSelected { get; set; }
    }
}