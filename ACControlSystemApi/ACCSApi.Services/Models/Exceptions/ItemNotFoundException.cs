using System;

namespace ACControlSystemApi.Services.Models.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message = "Item not found!") : base(message)
        {

        }
    }
}
