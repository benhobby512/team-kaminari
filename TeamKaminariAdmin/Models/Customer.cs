//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamKaminariAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Attendances = new HashSet<Attendance>();
            this.CustomerBelts = new HashSet<CustomerBelt>();
            this.EmergencyContacts = new HashSet<EmergencyContact>();
        }
    
        public System.Guid CustomerId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
        public string EmailAddress { get; set; }
        public string MedicalInfo { get; set; }
        public string LicenseNo { get; set; }
        public bool LiabilityWaiver { get; set; }
        public bool PhotoRelease { get; set; }
        public bool PayingMonthly { get; set; }
        public bool LicensePaid { get; set; }
        public Nullable<System.DateTime> LicenseResetDate { get; set; }
        public Nullable<System.Guid> EmergencyContactId { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public Nullable<System.DateTime> Dob { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attendance> Attendances { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerBelt> CustomerBelts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
    }
}
