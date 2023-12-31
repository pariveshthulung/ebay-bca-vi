﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ebay.Models;

public class CartItem
{
    public int id { get; set; }
    public int Cart_id { get; set; }
    [ForeignKey("Cart_id")]
    public virtual Cart? Cart { get; set; }
    public int Product_id { get; set; }
    [ForeignKey("Product_id")]
    public virtual Product? Product { get; set; }
    public int Quantity { get; set; }
    public bool Checked { get; set; }
    public DateTime AddedDate { get; set; } = DateTime.Now;

}
