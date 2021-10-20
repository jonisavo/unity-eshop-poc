using System;

namespace EshopPOC.API
{
    [Serializable]
    public class User
    {
        public string id;

        public string name;

        public string email;

        public string phone;

        public bool isAdmin;

        public string street;

        public string apartment;

        public string zip;

        public string city;

        public string country;

        public DateTime dateRegistered;

        public string token = "";
    }
}