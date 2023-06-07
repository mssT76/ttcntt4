using Microsoft.AspNetCore.Mvc;
using WebVPP.Extension;
using WebVPP.ModelViews;

namespace WebVPP.Controllers.Components
{
	public class NumberCartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("GioHang");
			return View(cart);
		}
	}
}
