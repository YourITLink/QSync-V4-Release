//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QSync
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class invitem
    {
        public int ItemNumber { get; set; }
        public int InvoiceNumber { get; set; }
        public string Item { get; set; }
        public Nullable<int> QTY { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual invoice invoice { get; set; }
    }
}
