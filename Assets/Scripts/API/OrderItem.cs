using System;

namespace EshopPOC.API
{
    [Serializable]
    public class OrderItem
    {
        public string product;

        public uint quantity;

        public OrderItem(string productId, uint quantity)
        {
            this.product = productId;
            this.quantity = quantity;
        }
    }
}