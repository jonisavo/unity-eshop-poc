using System.Collections;
using System.Collections.Generic;
using EshopPOC.API;
using UnityEngine;

namespace EshopPOC
{
    public class OrderManager : MonoBehaviour
    {
        private readonly Dictionary<Product, uint> _productsInOrder = new Dictionary<Product, uint>();

        public delegate void OnOrderResetDelegate();

        public event OnOrderResetDelegate OnOrderReset;

        public void AddToOrder(Product product, uint amount)
        {
            if (_productsInOrder.ContainsKey(product))
                _productsInOrder[product] += amount;
            else
                _productsInOrder[product] = amount;
        }

        public void RemoveFromOrder(Product product, uint amount)
        {
            if (!_productsInOrder.ContainsKey(product))
                return;

            _productsInOrder[product] -= (uint) Mathf.Clamp((int) amount, 0, (int) _productsInOrder[product]);

            if (_productsInOrder[product] == 0)
                _productsInOrder.Remove(product);
        }

        public uint GetCountInOrder(Product product)
        {
            return !_productsInOrder.ContainsKey(product) ? 0 : _productsInOrder[product];
        }

        public void MakeOrder()
        {
            if (UserManager.User == null || _productsInOrder.Count == 0)
                return;
            
            StartCoroutine(MakeOrderCoroutine());
        }

        private IEnumerator MakeOrderCoroutine()
        {
            var postOrderReq = EshopAPI.Post("/orders", new Dictionary<string, object>
            {
                { "orderItems", GetItemsFromDictionary(_productsInOrder) },
                { "shippingAddress1", "Karaportti 1" }, // Metropolia Karamalmi Campus :)
                { "city", "Espoo" },
                { "zip", "02610" },
                { "country", "Finland" },
                { "user", UserManager.User.id }
            });
            
            EshopAPI.AuthorizeRequest(postOrderReq, UserManager.User.token);

            yield return postOrderReq.SendWebRequest();

            var res = EshopAPI.ParseRequest<Order>(postOrderReq);

            if (!res.success)
            {
                Debug.LogError(res.error);
                yield break;
            }

            _productsInOrder.Clear();

            OnOrderReset?.Invoke();
        }

        private OrderItem[] GetItemsFromDictionary(Dictionary<Product, uint> dict)
        {
            var items = new OrderItem[dict.Count];

            uint i = 0;

            foreach (var dictItem in dict)
                items[i++] = new OrderItem(dictItem.Key.id, dictItem.Value);

            return items;
        }
    }
}