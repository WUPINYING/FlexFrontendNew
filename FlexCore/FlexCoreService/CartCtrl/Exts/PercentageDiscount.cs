﻿using FlexCoreService.CartCtrl.Models.vm;

namespace FlexCoreService.CartCtrl.Exts
{
    public class PercentageDiscount : BaseDiscountStrategy
    {
        public PercentageDiscount(ProductDiscountVM vm)
        {
            
        }
        public override IEnumerable<Discount> Process(CartContext cartItems)
        {
            throw new NotImplementedException();
        }
    }
}
