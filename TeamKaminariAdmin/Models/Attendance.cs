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
    
    public partial class Attendance
    {
        public System.Guid AttendanceId { get; set; }
        public System.Guid CustomerId { get; set; }
        public System.Guid SessionId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Session Session { get; set; }
    }
}
