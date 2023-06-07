using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVPP.Extension;
using WebVPP.ModelViews;

namespace WebVPP.Controllers
{
    public class CheckoutController : Controller
    {

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.GetObjectFromJson<List<CartItem>>("GioHang");
                if (gh == default(List<CartItem>))
                {
                    gh = new List<CartItem>();
                }
                return gh;
            }
        }

        [Route("checkout.html")]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("GioHang");
            ViewBag.GioHang = cart;
            return View();
        }
    }
}
