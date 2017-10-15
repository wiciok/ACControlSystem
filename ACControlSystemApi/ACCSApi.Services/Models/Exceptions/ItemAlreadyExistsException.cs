using System;

namespace ACCSApi.Services.Models.Exceptions
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException(string message = "Item already exists!") : base(message)
        {

        }
    }
}
