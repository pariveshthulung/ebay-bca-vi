﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ebay.Models;

public class ProductImages
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
    public string? ImageURl { get; set; }

}
