//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OTO.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rating
    {
        public int ScoreID { get; set; }
        public Nullable<int> f_id { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
    
        public virtual Firma Firma { get; set; }
    }
}
