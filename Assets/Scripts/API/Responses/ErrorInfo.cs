using System;

namespace EshopPOC.API
{
    [Serializable]
    public class ErrorInfo
    {
        public string _toString;
        public ErrorCode _code;

        public override string ToString()
        {
            return $"Error {_code}: ${_toString}";
        }
    }
}