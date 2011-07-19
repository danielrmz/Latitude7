using System;

namespace Latitude7.API.Storage
{
    /// <summary>
    /// Interface that defines the storage provider 
    /// for the data used by the API
    /// </summary>
    public interface IStorageContainer<T>
    {
        T Get(string key);
        bool Set(string key, T obj);
        bool Exists(string key);
        string GetHashCode(string key);
    }
}
