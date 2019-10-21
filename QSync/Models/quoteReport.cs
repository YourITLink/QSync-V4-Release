using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSync.Models
{
    public partial class quoteReportA
    {
        public quoteReportA()
        {

        }
        public class ReportingQuote
        {
            public int QuoteNumber { get; set; }
            public string Representative { get; set; }
            public string repMobile { get; set; }
            public string repID { get; set; }
            public Nullable<System.DateTime> Date1 { get; set; }
            public Nullable<int> MasterID { get; set; }
            public string Company { get; set; }
            public string Address { get; set; }
            public string Suburb { get; set; }
            public string State { get; set; }
            public Nullable<int> PostCode { get; set; }
            public string ContactPerson { get; set; }
            public string Phone { get; set; }
            public string Fax { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string JobLocation { get; set; }
            public string StrataNo { get; set; }
            public string RefNo { get; set; }
            public string SiteContact { get; set; }
            public string SCPhone { get; set; }
            public string SCMobile { get; set; }
            public string SCEmail { get; set; }
            public byte[] Attachments { get; set; }
            public Nullable<int> COD { get; set; }
            public Nullable<int> SvnDays { get; set; }
            public Nullable<int> OrderNo { get; set; }
            public Nullable<int> PROGRESSPAYMENT { get; set; }
            public Nullable<int> Induction { get; set; }
            public string Inductiontime { get; set; }
            public Nullable<int> SWMS { get; set; }
            public string App { get; set; }
            public string Notes { get; set; }
            public string Info { get; set; }
            public System.DateTime ModifyStamp { get; set; }
                      }


    }
}
