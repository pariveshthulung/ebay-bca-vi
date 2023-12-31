﻿using ebay.Models;
using ebay.ViewModel;

namespace ebay.Repository.IRepository;

public interface IProductRepository : IRepository<Product>
{
    ProductUpdateVm UpdateDisplay(int? id);
    int AddAsync (ProductAddVm vm);
    Task Update(int id,ProductUpdateVm vm);
}
