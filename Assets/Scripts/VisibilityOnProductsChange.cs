using EshopPOC.API;

namespace EshopPOC
{
    public class VisibilityOnProductsChange : VisibilityChange
    {
        private void OnEnable() => StoreItemManager.OnProductsChange += HandleProductsChange;

        private void OnDisable() => StoreItemManager.OnProductsChange -= HandleProductsChange;
        
        private void HandleProductsChange(Product[] products)
        {
            StopAllCoroutines();

            if (products.Length == 0)
                HandleNoProducts();
            else
                HandleProducts();
        }

        private void HandleNoProducts()
        {
            StartCoroutine(action == Action.FadeIn ? HideCoroutine() : ShowCoroutine());
        }

        private void HandleProducts()
        {
            StartCoroutine(action == Action.FadeIn ? ShowCoroutine() : HideCoroutine());
        }
    }
}