using System;
#if NET40
using System.Dynamic;
#endif

namespace Hammock.Serialization
{
    public interface IDeserializer
    {
        object Deserialize(RestResponse response, Type type);
        T Deserialize<T>(RestResponse<T> response);
#if NET40
        dynamic DeserializeDynamic(RestResponse<dynamic> response);
#endif
    }
}