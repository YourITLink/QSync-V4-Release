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
    
    public partial class quoteitem
    {
        public int ItemNumber { get; set; }
        public string QuoteNumber { get; set; }
        public string ItemDescription { get; set; }
        public string Details { get; set; }
        public Nullable<double> Price { get; set; }
        public int QNL { get; set; }
        public System.DateTime ModifyStamp { get; set; }
    
        public virtual quote quote { get; set; }
    }
}
