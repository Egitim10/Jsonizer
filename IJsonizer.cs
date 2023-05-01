using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsonizer
{
    public interface IJsonizer
    {
        string JsonizerToString<T>(T obj);

        Task<string> JsonizerToStringAsync<T>(T obj);

        T? JsonizertoObject<T>(string jsonString);

        Task<T?> JsonizertoObjectAsync<T>(string jsonString);

    }
}
