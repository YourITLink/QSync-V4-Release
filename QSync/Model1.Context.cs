﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class AbleDBEntity : DbContext
    {
        public AbleDBEntity()
            : base("name=AbleDBEntity")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C06mm> C06mm { get; set; }
        public virtual DbSet<C08mm> C08mm { get; set; }
        public virtual DbSet<C10mm> C10mm { get; set; }
        public virtual DbSet<AluminiumShutter> AluminiumShutters { get; set; }
        public virtual DbSet<BuildChanx> BuildChanges { get; set; }
        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<invitem> invitems { get; set; }
        public virtual DbSet<invoice> invoices { get; set; }
        public virtual DbSet<message> messages { get; set; }
        public virtual DbSet<nswpc> nswpcs { get; set; }
        public virtual DbSet<quoteitem> quoteitems { get; set; }
        public virtual DbSet<quote> quotes { get; set; }
        public virtual DbSet<service> services { get; set; }
        public virtual DbSet<shuttersheet> shuttersheets { get; set; }
        public virtual DbSet<site> sites { get; set; }
        public virtual DbSet<staff> staffs { get; set; }
        public virtual DbSet<state> states { get; set; }
        public virtual DbSet<tandc> tandcs { get; set; }
        public virtual DbSet<variable> variables { get; set; }
        public virtual DbSet<customercontmaster> customercontmasters { get; set; }
        public virtual DbSet<customermaster> customermasters { get; set; }
        public virtual DbSet<qtPreRepID> qtPreRepIDs { get; set; }
        public virtual DbSet<AbleQuote> AbleQuotes { get; set; }
        public virtual DbSet<AppAccess> AppAccesses { get; set; }
        public virtual DbSet<QuoteDateCorrection> QuoteDateCorrections { get; set; }
        public virtual DbSet<QuoteFL> QuoteFLs { get; set; }
        public virtual DbSet<QuoteItemsFL> QuoteItemsFLs { get; set; }
    
        public virtual ObjectResult<get_easyLoad_Result> get_easyLoad(Nullable<int> page_from, Nullable<int> page_size)
        {
            var page_fromParameter = page_from.HasValue ?
                new ObjectParameter("page_from", page_from) :
                new ObjectParameter("page_from", typeof(int));
    
            var page_sizeParameter = page_size.HasValue ?
                new ObjectParameter("page_size", page_size) :
                new ObjectParameter("page_size", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<get_easyLoad_Result>("get_easyLoad", page_fromParameter, page_sizeParameter);
        }
    
        public virtual ObjectResult<QUOTESLOADING_Result> QUOTESLOADING()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<QUOTESLOADING_Result>("QUOTESLOADING");
        }
    }
}
