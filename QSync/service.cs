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
    
    public partial class service
    {
        public int SiteNo { get; set; }
        public int CustNo { get; set; }
        public int SvcNo { get; set; }
        public string Fixer { get; set; }
        public string SvcDate { get; set; }
        public Nullable<int> c1 { get; set; }
        public Nullable<int> c2 { get; set; }
        public Nullable<int> c3 { get; set; }
        public Nullable<int> c4 { get; set; }
        public Nullable<int> c5 { get; set; }
        public Nullable<int> c6 { get; set; }
        public Nullable<int> c7 { get; set; }
        public Nullable<int> c8 { get; set; }
        public Nullable<int> c9 { get; set; }
        public Nullable<int> c10 { get; set; }
        public Nullable<int> c11 { get; set; }
        public Nullable<int> c12 { get; set; }
        public Nullable<int> c13 { get; set; }
        public Nullable<int> c14 { get; set; }
        public Nullable<int> c15 { get; set; }
        public Nullable<int> c16 { get; set; }
        public Nullable<int> c17 { get; set; }
        public Nullable<int> Asmt { get; set; }
        public Nullable<int> rg { get; set; }
        public Nullable<int> rfg { get; set; }
        public Nullable<int> stg { get; set; }
        public Nullable<int> repsl { get; set; }
        public Nullable<int> strb { get; set; }
        public Nullable<int> refc { get; set; }
        public Nullable<int> chc { get; set; }
        public Nullable<int> chf { get; set; }
        public string fixerrep { get; set; }
        public Nullable<int> tr1 { get; set; }
        public Nullable<int> tr2 { get; set; }
        public Nullable<int> tr3 { get; set; }
        public Nullable<int> inv { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual site site { get; set; }
    }
}
