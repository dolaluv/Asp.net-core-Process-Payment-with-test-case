using Microsoft.EntityFrameworkCore;
using PaymentAccessPortal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentProcessTest
{
   public class PaymentAccessPortalContextFake : DbContext
    {
        public PaymentAccessPortalContextFake(DbContextOptions options) : base(options) { }

        public DbSet<PaymentCardDetails> PaymentCardDetails { get; set; }
    }
}
