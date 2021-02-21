using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.Model
{
    public partial class PaymentCardDetails
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string SecurityCode { get; set; }
        public Decimal Amount { get; set; }

        public string PaymentState { get; set; }
 


    }
        
}
