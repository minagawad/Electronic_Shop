using System;
using System.Collections.Generic;

namespace Electronic_Shop.Buyers.Models
{
    public class OrderModel
    {
        public string UserId { get; set; }
        public IList<ItemsModel> Items { get; set; }
    }
}
