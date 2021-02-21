using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentAccessPortal.Helpers;
using PaymentAccessPortal.Model;
using PaymentAccessPortal.Model.Data;
using PaymentAccessPortal.PaymentGateway;
 

namespace PaymentAccessPortal.Controllers
{
  //  [Route("api/[controller]")]
   // [ApiController]
    public class PaymentController : Controller
    {

        private IPaymentGateway _IPaymentGateway;
        private ICheapPaymentGateway _ICheapPaymentGateway;
        private IExpensivePaymentGateway _IExpensivePaymentGateway;
        private IPremiumPaymentService _IPremiumPaymentService;
        public PaymentController(IPaymentGateway paymentGateway, ICheapPaymentGateway cheapPaymentGateway, IExpensivePaymentGateway expensivePaymentGateway, IPremiumPaymentService premiumPaymentService)
        {

            _IPaymentGateway = paymentGateway;
            _ICheapPaymentGateway= cheapPaymentGateway;
            _IExpensivePaymentGateway = expensivePaymentGateway;
            _IPremiumPaymentService = premiumPaymentService;
        }

        

        // GET: api/Payment
        [HttpPost]
        [Route("api/ProcessPayment")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentCardDetails ClientCardDetails)
        {
            StatusMessages statusMessages = new StatusMessages();
            try
            {


               
                PaymentCardDetails _PaymentCardDetails = new PaymentCardDetails();

                var ValidateCard = ValidateCreditCard(ClientCardDetails.CreditCardNumber);

                if (ValidateCard == false)
                {

                    statusMessages.Status = "failed";
                    statusMessages.Message = "Invalid credit card Details";
                    return BadRequest(statusMessages);

                }
                else if (ValidateCardDetails(ClientCardDetails) == false)
                {

                    statusMessages.Status = "failed";
                    statusMessages.Message = "The request is invalid:";
                    return BadRequest(statusMessages);
                }


                else
                {

                    if (ClientCardDetails.Amount < 20)
                    {
                        var MakePayment = await _ICheapPaymentGateway.CheapPaymentGateway(ClientCardDetails);

                        if (MakePayment.Status == "Success")
                        {
                            ClientCardDetails.PaymentState = "processed";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            statusMessages.Status = "Success";
                            statusMessages.Message = MakePayment.Message;
                        }
                        else
                        {
                            ClientCardDetails.PaymentState = "failed";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            statusMessages.Status = "failed";
                            statusMessages.Message = MakePayment.Message;
                            return BadRequest(statusMessages);

                        }


                    }
                    else if (ClientCardDetails.Amount > 20 && ClientCardDetails.Amount < 500)
                    {
                        var MakePayment = await _IExpensivePaymentGateway.ExpensivePaymentGateway(ClientCardDetails);

                        if (MakePayment.Status == "Success")
                        {
                            ClientCardDetails.PaymentState = "processed";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            statusMessages.Status = "Success";
                            statusMessages.Message = MakePayment.Message;
                        }
                        else
                        {
                            ClientCardDetails.PaymentState = "failed";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            statusMessages.Status = "failed";
                            statusMessages.Message = MakePayment.Message;
                            return BadRequest(statusMessages);
                        }
                    }
                    else if (ClientCardDetails.Amount > 500)
                    {
                        var MakePayment = await _IPremiumPaymentService.PremiumPaymentService(ClientCardDetails);

                        if (MakePayment.Status == "Success")
                        {
                            ClientCardDetails.PaymentState = "processed";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            statusMessages.Status = "Success";
                            statusMessages.Message = MakePayment.Message;
                        }
                        else
                        {
                            statusMessages.Status = "failed";
                            statusMessages.Message = MakePayment.Message;
                            ClientCardDetails.PaymentState = "pending";
                            await _IPaymentGateway.SavePaymentStatus(ClientCardDetails);
                            return BadRequest(statusMessages);
                        }

                    }



                }



                return Ok(statusMessages);


            }
            catch(Exception ex)
            {

                var result = StatusCode(StatusCodes.Status500InternalServerError, ex);
                return result;
            }
}
        public bool ValidateCreditCard(string creditCardNumber)
        {

            if (creditCardNumber == null || creditCardNumber == "")
                return false;
            //Strip any non-numeric values
            creditCardNumber = Regex.Replace(creditCardNumber, @"[^\d]", "");

            //Build your Regular Expression
            Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");

            //Return if it was a match or not
            return expression.IsMatch(creditCardNumber);
        }


        public bool ValidateCardDetails(PaymentCardDetails ClientCardDetails)
        {
            
            var CardExpDate = Convert.ToDateTime(ClientCardDetails.ExpirationDate);
            var CurrentDate = Convert.ToDateTime(DateTime.Today);
 

            if (ClientCardDetails.CardHolder == "" || ClientCardDetails.CardHolder == null)
            {

                return false;

            }
            else if (CurrentDate > CardExpDate)
            {

                return false;

            }
            else if (ClientCardDetails.Amount < 0)
            {

                return false;

            }


            return true;
        }

 
    }
}
