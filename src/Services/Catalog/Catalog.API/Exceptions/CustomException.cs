namespace Catalog.API.Exceptions
{
    public class CustomException(string message, int? internalCode = 500) : Exception(message)
    {
        public int? InternalCode { get; set; } = internalCode;
        public static CustomException NotFound(string msg) => new(msg, 404);
    }

}