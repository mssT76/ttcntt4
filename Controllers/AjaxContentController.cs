using Microsoft.AspNetCore.Mvc;

namespace WebVPP.Controllers
{
	public class AjaxContentController : Controller
	{
		public IActionResult HeaderFavourites()
		{
			return ViewComponent("NumberCart");
		}
	}
}
