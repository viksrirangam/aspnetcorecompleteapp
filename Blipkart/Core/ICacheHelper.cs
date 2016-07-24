using System;
using System.Collections.Generic;

namespace Blipkart.Core.Infra
{
    public interface ICacheHelper<T>
    {
        bool TryGet(string key, out T item);
        void Set(string key, T item);
        void Remove(string key);
    }
}
