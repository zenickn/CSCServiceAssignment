using WebRole1.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebRole1.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Stripe()
        {
            //ViewBag.Title = "Home Page";

            //return View();
            ViewBag.Message = "Learn how to process payments with Stripe";
            ViewBag.ReturnUrl = "../PaymentSuccess.html";
            return View(new StripeChargeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var chargeId = await ProcessPayment(model);
            // You should do something with the chargeId --> Persist it maybe?

            return View("PaymentSuccess");
        }

        private static async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {

                var myCharge2 = new StripeChargeCreateOptions();
                myCharge2.Amount = (int)(model.Amount * 100);
                myCharge2.Currency = "sgd";
                myCharge2.Description = "Description for test charge";
                myCharge2.SourceTokenOrExistingSourceId = model.Token.ToString();

                String msg = "";

                var key = "sk_test_770dfGjXDPlIv7RakGofuTUt";

                var chargeService = new StripeChargeService(key);

                try
                {
                    var stripeCharge = chargeService.Create(myCharge2);
                    msg = stripeCharge.Id;
                }
                catch (StripeException e)
                {
                    msg = e.ToString();
                }


                return msg;
            });
        }

        public ActionResult PaymentSuccess()
        {
            return View();
        }

    }
}
