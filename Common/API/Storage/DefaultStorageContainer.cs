using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Latitude7.API.Storage
{
    public class DefaultStorageContainer<T> : IStorageContainer<T>
    {
        private static Dictionary<string, T> _localStorage = new Dictionary<string, T>();

        #region IStorageContainer Members

        /// <summary>
        /// Returns the specified element of the storage.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get(string key) 
        {
            key = this.GetHashCode(key);
            return _localStorage[key];
        }

        /// <summary>
        /// Saves an object on the default storage
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set(string key, T obj)
        {
            key = this.GetHashCode(key);
            if (this.Exists(key))
            {
                _localStorage[key] = obj;
            }
            else
            {
                _localStorage.Add(key, obj);
            }
            return true;
        }

        /// <summary>
        /// Checks if a specified element exists.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            key = this.GetHashCode(key);
            return _localStorage.ContainsKey(key);
        }

        /// <summary>
        /// Gets a unique key identifier for local storage, using the key 
        /// sent by the user as a seed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetHashCode(string key)
        {
            Encoding enc = Encoding.UTF8;
            byte[] buffer = enc.GetBytes(key);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }

        #endregion
    }
}
