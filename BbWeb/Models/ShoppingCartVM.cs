﻿namespace BbWeb.Models
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        //public double CartTotal { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
