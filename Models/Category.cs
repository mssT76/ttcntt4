using System;
using System.Collections.Generic;

namespace WebVPP.Models;

public partial class Category
{
    public int CatId { get; set; }

    public string? CatName { get; set; }

    public string? Desciption { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
