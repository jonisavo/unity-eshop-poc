using System;

namespace EshopPOC.API
{
    [Serializable]
    public class Response<T> where T : class
    {
        public bool success;
        public ErrorInfo error;
        public T result;
    }
}