using Microsoft.AspNetCore.Mvc;
using PaymentService.Dto;
using Stripe.Checkout;

namespace PaymentService.Controllers
{
    [Route("api/create-checkout-session")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        [HttpPost("create")]
        public ActionResult Create([FromBody] PaymentReceiveDto paymentreceiveDto)
        {
            var domain = "http://localhost:3000/return";
            var options = new SessionCreateOptions
            {
                UiMode = "embedded",
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    //Price = "prod_QJGJshnFcEGwXl",
                    PriceData = new SessionLineItemPriceDataOptions
                     {
                       UnitAmount = Convert.ToInt32(paymentreceiveDto.value)*100,
                       Currency = "inr",
                       ProductData = new SessionLineItemPriceDataProductDataOptions
                       {
                          Name = "For"+" "+paymentreceiveDto.vehiclenumber+" "+"Renting",
                       },
                     },
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                ReturnUrl = domain ,
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { clientSecret = session.RawJObject["client_secret"] });
        }
    }
}
