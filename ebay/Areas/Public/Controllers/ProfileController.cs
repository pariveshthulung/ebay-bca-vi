﻿using AspNetCoreHero.ToastNotification.Abstractions;
using ebay.Data;
using ebay.Provider.Interface;
using ebay.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ebay.Areas.Public.Controllers;
[Area("Public")]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly INotyfService _notifyService;

    public ProfileController(ApplicationDbContext context, ICurrentUserProvider currentUserProvider, INotyfService notifyService)
    {
        _context = context;
        _currentUserProvider = currentUserProvider;
        _notifyService = notifyService;
    }
    public async Task<IActionResult> Index(CheckOutVm vm)
    {
        // get current user
        vm.User_id = _currentUserProvider.GetCurrentUserId();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.User_id);

        vm.FirstName = user.FirstName;
        vm.LastName = user.LastName;
        vm.PhoneNo = user.PhoneNo;
        vm.Email = user.Email;

        // get current's user address
        var address = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id && x.Is_Default == true);
        if (address != null)
        {
            vm.Address_Line = address.Address_Line;
            vm.Region = address.Region;
            vm.Postal_Code = address.Postal_Code;
            vm.Landmark = address.Landmark;
            vm.City = address.City;
            // vm.Countries = _context.Countries.Where(x => x.id == address.Country_id).ToList();
            vm.CountryId = address.Country_id;
        }
            vm.Countries = await _context.Countries.ToListAsync();


        return View(vm);
    }
    [HttpPost]
    [ActionName("Index")]
    public async Task<IActionResult> IndexPost(CheckOutVm vm)
    {
        // get current user
        vm.User_id = _currentUserProvider.GetCurrentUserId();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == vm.User_id);

        user.FirstName = vm.FirstName;
        user.LastName = vm.LastName;
        user.PhoneNo = vm.PhoneNo;


        // update current's user address
        var addressFrmDb = await _context.Addresses.FirstOrDefaultAsync(x => x.User_id == vm.User_id);
        addressFrmDb.Address_Line = vm.Address_Line;
        addressFrmDb.Landmark = vm.Landmark;
        addressFrmDb.Region = vm.Region;
        addressFrmDb.Postal_Code = vm.Postal_Code;
        addressFrmDb.City = vm.City;
        addressFrmDb.Country = await _context.Countries.Where(x => x.id == vm.CountryId).FirstOrDefaultAsync();
        _context.SaveChanges();
        _notifyService.Success("Profile updated successfull!!");

        return RedirectToAction(nameof(Index), nameof(Public));
    }
    public IActionResult Myorder(OrderMangementVm vm)
    {
        // get current user 
        vm.UserId = _currentUserProvider.GetCurrentUserId();
        vm.OrderList = _context.Orders.Where(x => x.User_id == vm.UserId).ToList();
        vm.OrderIdList = _context.Orders.Where(x => x.User_id == vm.UserId).Select(x => x.id).ToList();
        vm.OrderItemsList = _context.OrderItems.Where(x => vm.OrderIdList.Contains(x.Order_id)).Include(x => x.Product).Include(x => x.Order).ToList();
        return View(vm);
    }
    // [HttpPost]
    // [ActionName("Myorder")]
    // public IActionResult CancelOrder(int orderId, int orderItemId)
    // {
    //     try
    //     {
    //         using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    //         {
    //             var orderFromDb = _context.Orders.FirstOrDefault(x => x.id == orderId);
    //             var orderItemsFrmDb = _context.OrderItems.Where(x => x.id == orderItemId).FirstOrDefault();
    //             if (orderItemsFrmDb.PaymentStatus == "Approved")
    //             {
    //                 var options = new RefundCreateOptions
    //                 {
    //                     PaymentIntent = orderFromDb.PaymentIntentId,
    //                     Amount = (long?)orderItemsFrmDb.Price * 100 * orderItemsFrmDb.Quantity
    //                 };
    //                 var service = new RefundService();
    //                 service.Create(options);
    //                 UpdateStatus(orderItemsFrmDb.id, OrderStatusConstants.Cancelled, PaymentStatusConstant.Refund);
    //             }
    //             _context.SaveChanges();
    //             _notifyService.Success("Order cancelLed Sucessfully!!!");

    //             tx.Complete();
    //             return RedirectToAction("Myorder", "Profile");
    //         }
    //     }
    //     catch (Exception e)
    //     {
    //         return Content(e.Message);
    //     }

    // }



}
