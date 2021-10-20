using System.Collections;
using System.Collections.Generic;
using EshopPOC.API;
using UnityEngine;

namespace EshopPOC
{
    public class StoreItemManager : MonoBehaviour
    {
        public static readonly Dictionary<string, Product> Products = new Dictionary<string, Product>();

        public GameObject storeItemObject;

        public GameObject storeItemArea;

        public delegate void OnProductsChangeDelegate(Product[] products);

        public static event OnProductsChangeDelegate OnProductsChange;

        private void Awake()
        {
            StartCoroutine(GetStoreItems());
        }
        
        private IEnumerator GetStoreItems()
        {
            var getProductsReq = EshopAPI.Get("/products");
            
            yield return getProductsReq.SendWebRequest();

            var productRes = EshopAPI.ParseRequest<Product[]>(getProductsReq);
            
            if (!productRes.success)
            {
                Debug.LogError(productRes.error);
                yield break;
            }

            var products = productRes.result;

            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];
                
                Products[product.id] = product;
                
                var position = new Vector3(20f + 280f * (i % 6), -200f * (i / 6));

                var obj = Instantiate(storeItemObject, position, Quaternion.identity);
                
                obj.transform.SetParent(storeItemArea.transform, false);

                obj.GetComponent<StoreItemObject>().Initialize(product);
            }
            
            OnProductsChange?.Invoke(products);
        }
    }
}