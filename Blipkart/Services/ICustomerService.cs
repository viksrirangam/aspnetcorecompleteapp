using System;
using System.Collections.Generic;
using System.Linq;

using Blipkart.Model;

namespace Blipkart.Service
{
    public interface ICustomerService
    {
        long GetIdByName(string Name);
    }
}
