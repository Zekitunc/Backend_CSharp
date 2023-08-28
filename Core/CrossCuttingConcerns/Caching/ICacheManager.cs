using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        void Add(string key, object value, int duration); //her şeyin basei obje ataması
        T Get<T> (string key); //Generic
        object Get(string key);

        bool IsAdd (string key);
        void Remove (string key);
        void RemoveByPattern (string pattern); //içinde kategori olanı sil gibi
    }
}
