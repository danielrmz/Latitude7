using System;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

using ServiceStack.Text;
using ServiceStack.Text.Json;

namespace Common7.Models
{
    public class Loader
    {
        public static T Parse<T>(string jsonString)
        {

            return JsonSerializer.DeserializeFromString<T>(jsonString);
            /*
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                //parse into jsonser.
                var ser = new DataContractJsonSerializer(typeof(T));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }*/
        }

        public static string ToJson<T>(T obj)
        {
            return JsonSerializer.SerializeToString<T>(obj);
            /*
            DataContractJsonSerializer serializer = new  DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            return Encoding.Default.GetString(ms.ToArray());*/
        }
    }
}
