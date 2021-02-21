using PaymentAccessPortal.Helpers;
using PaymentAccessPortal.Model;
using PaymentAccessPortal.Model.Data;
using PaymentAccessPortal.PaymentGateway;
using PaymentAccessPortal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessTest
{
    public class PaymentServicesFake : ICheapPaymentGateway, IExpensivePaymentGateway, IPremiumPaymentService, IPaymentGateway
    {
        public PaymentAccessPortalContext _PaymentAccessPortalContext;

        public PaymentServicesFake(PaymentAccessPortalContext paymentAccessPortalContext)
        {
            _PaymentAccessPortalContext = paymentAccessPortalContext;
        }

        public PaymentServicesFake()
        {

        }

        async public Task<PaymentCardDetails> SampleCardDetails(PaymentCardDetails _PaymentCardDetails)
        {
            var paymentCardDetails = await Task.FromResult(PaymentSampleCardDetails._PaymentSampleCardDetails.
                                                             FirstOrDefault(x => x.CreditCardNumber == _PaymentCardDetails.CreditCardNumber && x.CardHolder == _PaymentCardDetails.CardHolder && x.Amount > _PaymentCardDetails.Amount));

            if (paymentCardDetails == null)
                return null;

            return await Task.FromResult(paymentCardDetails);
        }

        async public Task<StatusMessages> CheapPaymentGateway(PaymentCardDetails _PaymentCardDetails)
        {
            StatusMessages _StatusMessages = new StatusMessages();

            var _SampleCardDetails = await SampleCardDetails(_PaymentCardDetails);
            if (_SampleCardDetails != null)
            {
                _StatusMessages.Status = "Success";
                _StatusMessages.Message = "Success";
            }
            else
            {
                _StatusMessages.Status = "Failed";
                _StatusMessages.Message = "Not Found";
            }


            return _StatusMessages;
        }

        async public Task<StatusMessages> ExpensivePaymentGateway(PaymentCardDetails _PaymentCardDetails)
        {
            StatusMessages _StatusMessages = new StatusMessages();

            var _SampleCardDetails = await SampleCardDetails(_PaymentCardDetails);
            if (_SampleCardDetails != null)
            {
                _StatusMessages.Status = "Success";
                _StatusMessages.Message = "Success";
            }
            else
            {
                var TryCheapPaymentGateway = await CheapPaymentGateway(_PaymentCardDetails);
                if (TryCheapPaymentGateway != null)
                {
                    _StatusMessages.Status = "Success";
                    _StatusMessages.Message = "Success";
                }
                else
                {
                    _StatusMessages.Status = "Failed";
                    _StatusMessages.Message = "Not Found";
                }

            }


            return _StatusMessages;
        }

        async public Task<StatusMessages> PremiumPaymentService(PaymentCardDetails _PaymentCardDetails)
        {
            StatusMessages _StatusMessages = new StatusMessages();

            var _SampleCardDetails = await SampleCardDetails(_PaymentCardDetails);
            if (_SampleCardDetails != null)
            {
                _StatusMessages.Status = "Success";
                _StatusMessages.Message = "Success";
            }
            else
            {
                for (var k = 0; k < 1; k++)
                {
                    _SampleCardDetails = await SampleCardDetails(_PaymentCardDetails);
                    if (_SampleCardDetails != null)
                    {
                        _StatusMessages.Status = "Success";
                        _StatusMessages.Message = "Success";
                        break;
                    }
                    else
                    {
                        _StatusMessages.Status = "Failed";
                        _StatusMessages.Message = "Not Found";
                    }
                }

            }


            return _StatusMessages;
        }

        async public Task<StatusMessages> SavePaymentStatus(PaymentCardDetails _PaymentCardDetails)
        {
            StatusMessages _StatusMessages = new StatusMessages();

            await _PaymentAccessPortalContext.AddAsync(_PaymentCardDetails);
            _PaymentAccessPortalContext.SaveChanges();

            return _StatusMessages;
        }

    }
}
