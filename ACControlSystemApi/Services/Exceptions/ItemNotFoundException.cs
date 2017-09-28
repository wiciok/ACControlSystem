using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACControlSystemApi.Services.Exceptions
{
    public class ItemNotFoundException: Exception
    {
        public ItemNotFoundException() : base("Item not found!")
        {

        }
    }
}
