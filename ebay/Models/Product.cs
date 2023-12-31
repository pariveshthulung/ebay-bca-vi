﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ebay.Models
{
	public class Product
	{
		public int id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		[Column(TypeName = "decimal(18,2)")] 

		public decimal Price { get; set; }
		public int Stock { get; set; }
		public string? Brand { get; set; }
		[DisplayName("Image")]
		public string? Product_image { get; set; }
		public virtual List<ProductCategory>? ProductCategories { get; set; }
		
		
		

	}
}

