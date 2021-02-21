using PaymentAccessPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAccessPortal.Services
{
    public class PaymentSampleCardDetails
    {
 
        public static List<PaymentCardDetails> _PaymentSampleCardDetails = new List<PaymentCardDetails> {

         new PaymentCardDetails{CreditCardNumber= "5105105105105100",CardHolder = "Dolapo",SecurityCode="12345",Amount=7000, ExpirationDate = Convert.ToDateTime("12/12/2021") },

        new PaymentCardDetails{CreditCardNumber= "5555555555554444",CardHolder = "iliasu",SecurityCode="12345",Amount=7000, ExpirationDate = Convert.ToDateTime("12/12/2021") },

        new PaymentCardDetails{CreditCardNumber= "4111111111111111",CardHolder = "muritala",SecurityCode="12345",Amount=7000,ExpirationDate = Convert.ToDateTime("12/12/2021")}

        };
       

     


    }
}
