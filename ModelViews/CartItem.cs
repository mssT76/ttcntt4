using WebVPP.Models;

namespace WebVPP.ModelViews
{
    public class CartItem
    {
            public Product product { get; set; }
            public int amount { get; set; }

            public int TotalMoney => amount * product.Price.Value;
    }
}
