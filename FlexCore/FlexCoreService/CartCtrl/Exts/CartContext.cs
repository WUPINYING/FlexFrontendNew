﻿using EFModels.Models;
using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts
{
    public class CartContext
    {
        public readonly IEnumerable<CartItemVM> CartItems;
        public readonly List<Discount> AppliedDiscounts = new List<Discount>();
        public decimal TotalPrice = 0m;
        public CartContext(IEnumerable<CartItemVM> vms)
        {
            CartItems = vms;
        }
    }
}
