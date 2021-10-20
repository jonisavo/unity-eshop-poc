using EshopPOC.API;
using UnityEngine;
using UnityEngine.UI;

namespace EshopPOC
{
    public class StoreItemObject : MonoBehaviour
    {
        public Text nameLabel;

        public Text descriptionLabel;

        public Text priceLabel;

        public Text orderCountLabel;

        public Text categoryLabel;

        private Product _item;

        private OrderManager _orderManager;

        private void Awake() => _orderManager =
            GameObject.Find("Order Manager").GetComponent<OrderManager>();

        private void OnEnable() => _orderManager.OnOrderReset += HandleOrderReset;

        private void OnDisable() => _orderManager.OnOrderReset -= HandleOrderReset;
        
        public void Initialize(Product item)
        {
            nameLabel.text = item.name;

            descriptionLabel.text = item.description;

            priceLabel.text = $"${item.price}";

            categoryLabel.text = item.category.name;

            _item = item;
        }

        public void AddToCart()
        {
            _orderManager.AddToOrder(_item, 1);

            orderCountLabel.text = _orderManager.GetCountInOrder(_item).ToString();
        }

        public void RemoveFromCart()
        {
            _orderManager.RemoveFromOrder(_item, 1);
            
            orderCountLabel.text = _orderManager.GetCountInOrder(_item).ToString();
        }

        private void HandleOrderReset()
        {
            orderCountLabel.text = _orderManager.GetCountInOrder(_item).ToString();
        }
    }
}