using System;

namespace EshopPOC.API
{
    [Serializable]
    public class Product
    {
        public string id;
        
        public string name;

        public string description;

        public string richDescription;

        public string image;

        public string[] images;

        public string brand;

        public float price;

        public Category category;

        public int stock;

        public float rating;

        public int numReviews;

        public bool isFeatured;

        public DateTime dateCreated;
    }
}