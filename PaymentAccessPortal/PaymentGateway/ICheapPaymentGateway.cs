using PaymentAccessPortal.Helpers;
using PaymentAccessPortal.Model;
using PaymentAccessPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.PaymentGateway
{
   public interface ICheapPaymentGateway
    {

        Task<StatusMessages> CheapPaymentGateway(PaymentCardDetails _PaymentCardDetails);

       
    }
}
