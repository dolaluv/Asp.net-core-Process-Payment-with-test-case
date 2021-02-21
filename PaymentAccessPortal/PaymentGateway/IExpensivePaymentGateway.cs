using PaymentAccessPortal.Helpers;
using PaymentAccessPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.PaymentGateway
{
   public interface IExpensivePaymentGateway
    {
        Task<StatusMessages> ExpensivePaymentGateway(PaymentCardDetails _PaymentCardDetails);
    }
}
