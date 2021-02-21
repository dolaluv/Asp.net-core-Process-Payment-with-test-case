using PaymentAccessPortal.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.Model.Data
{
   public  interface IPaymentGateway
    {

        Task<StatusMessages> SavePaymentStatus(PaymentCardDetails _PaymentCardDetails);
 
    }
}
