//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DeliveryApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VISITA_ASSIGN
    {
        public int visi_id { get; set; }
        public int cust_id { get; set; }
        public Nullable<int> inst_id { get; set; }
        public string visi_date { get; set; }
        public Nullable<int> visi_op { get; set; }
        public Nullable<int> visi_status { get; set; }
        public string visi_hora { get; set; }
        public Nullable<int> paqu_id { get; set; }
    }
}
