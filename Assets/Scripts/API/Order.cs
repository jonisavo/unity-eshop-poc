using System;

namespace EshopPOC.API
{
    [Serializable]
    public class Order
    {
        public OrderItem[] orderItems;
        public string shippingAddress1;
        public string shippingAddress2;
        public string city;
        public string country;
        public string phone;
        public string status;
        public float totalPrice;
        public string userId;
        public DateTime dateOrdered;
    }
}