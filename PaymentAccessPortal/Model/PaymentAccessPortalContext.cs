using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.Model
{
    public class PaymentAccessPortalContext: DbContext
    {
        public PaymentAccessPortalContext(DbContextOptions options) : base(options) { }

        public DbSet<PaymentCardDetails> PaymentCardDetails { get; set; }


    }
}
