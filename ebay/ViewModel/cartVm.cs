﻿using System.ComponentModel.DataAnnotations.Schema;
using ebay.Entity;
using ebay.Models;

namespace ebay.ViewModel;

public class cartVm
{
    public int? User_id { get; set; }
    public Product? Product;

    public int Cart_id { get; set; }
    public int Product_id { get; set; }
    public int Quantity { get; set; }
    public List<CartItem>? CartItemList ;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Subtotal { get; set; }
    public List<int>? Checked { get; set; }
    public double AverageReview { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<ProductImages>? ProductImages { get; set; }
}
