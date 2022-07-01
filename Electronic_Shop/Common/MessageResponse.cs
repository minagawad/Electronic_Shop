using System.Collections.Generic;

namespace Electronic_Shop.Common
{
    public class MessageResponse<T> where T : class
    {
        public MessageResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public T RequestedObject { get; set; }



    }
    public class MessageResponse
    {
        public MessageResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }

    }
}
