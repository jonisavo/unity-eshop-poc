using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace EshopPOC.API
{
    public static class EshopAPI
    {
        public const string Address = "http://localhost:3000/api/v1";

        public static string FullURL(string snippet) => Address + snippet;
        
        public static UnityWebRequest Get(string snippet) =>
            UnityWebRequest.Get(FullURL(snippet));
        
        public static UnityWebRequest Post(string snippet, Dictionary<string, object> fields)
        {
            var json = JsonConvert.SerializeObject(fields);

            var req = UnityWebRequest.Post(FullURL(snippet), json);
            
            req.SetRequestHeader("Content-Type", "application/json");
            
            req.uploadHandler.Dispose();

            req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            
            req.uploadHandler.contentType = "application/json";

            return req;
        }

        public static void AuthorizeRequest(UnityWebRequest req, string token) =>
            req.SetRequestHeader("Authorization", $"Bearer {token}");

        public static Response<T> ParseRequest<T>(UnityWebRequest req) where T : class
        {
            var text = req.downloadHandler.text;
            
            Debug.Log(text);
            
            return (Response<T>) JsonUtility.FromJson(text, typeof(Response<T>));
        }
    }
}