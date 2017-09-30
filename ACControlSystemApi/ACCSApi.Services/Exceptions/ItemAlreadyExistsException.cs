using System;

namespace ACControlSystemApi.Services.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException(string message = "Item already exists!") : base(message)
        {

        }
    }
}
