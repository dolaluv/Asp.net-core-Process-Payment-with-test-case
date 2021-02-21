using PaymentAccessPortal.Helpers;
using PaymentAccessPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.PaymentGateway
{
   public interface IPremiumPaymentService
    {
        Task<StatusMessages> PremiumPaymentService(PaymentCardDetails _PaymentCardDetails);
    }
}
