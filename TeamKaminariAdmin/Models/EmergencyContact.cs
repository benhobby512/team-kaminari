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
    
    public partial class EmergencyContact
    {
        public System.Guid EmergencyContactId { get; set; }
        public System.Guid CustomerId { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Tel1 { get; set; }
        public string Tel2 { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
