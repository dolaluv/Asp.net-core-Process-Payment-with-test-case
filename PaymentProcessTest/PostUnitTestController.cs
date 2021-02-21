using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAccessPortal.Controllers;
using PaymentAccessPortal.Model;
using PaymentAccessPortal.Model.Data;
using PaymentAccessPortal.PaymentGateway;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentProcessTest
{
   public class PostUnitTestController
    {
        PaymentController _controller;
        private IPaymentGateway _IPaymentGateway;
        private ICheapPaymentGateway _ICheapPaymentGateway;
        private IExpensivePaymentGateway _IExpensivePaymentGateway;
        private IPremiumPaymentService _IPremiumPaymentService;
        public static DbContextOptions<PaymentAccessPortalContext> dbContextOptions { get; }
        public static string connectionString = "Server=DEVPOWERSOFT3;Database=PaymentAccessPortal;Trusted_Connection=True;MultipleActiveResultSets=true";

        static PostUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<PaymentAccessPortalContext>()
                .UseSqlServer(connectionString)
                .Options;
        }
        public PostUnitTestController()
        {
            var context = new PaymentAccessPortalContext(dbContextOptions);
            _IPaymentGateway = new PaymentServicesFake(context);
            _ICheapPaymentGateway = new PaymentServicesFake(context);
            _IExpensivePaymentGateway = new PaymentServicesFake(context);
            _IPremiumPaymentService = new PaymentServicesFake(context);
            _controller = new PaymentController(_IPaymentGateway, _ICheapPaymentGateway, _IExpensivePaymentGateway, _IPremiumPaymentService);
        }
        [Fact]
        public async void ProcessPayment()
        {
            //Arrange  
          //  var controllerr = new PaymentController(_IPaymentGateway, _ICheapPaymentGateway, _IExpensivePaymentGateway, _IPremiumPaymentService);
            var _PaymentCardDetails = new PaymentCardDetails { CreditCardNumber = "5105105105105100", CardHolder = "Dolapo", SecurityCode = "12345", Amount = 30, ExpirationDate = Convert.ToDateTime("12/12/2021") };
           
            //Act  
            var data = await _controller.ProcessPayment(_PaymentCardDetails);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }
    }
}
