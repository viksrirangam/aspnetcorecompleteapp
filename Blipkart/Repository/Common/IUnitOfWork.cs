using System;
using System.Collections.Generic;
using System.Linq;

namespace Blipkart.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
