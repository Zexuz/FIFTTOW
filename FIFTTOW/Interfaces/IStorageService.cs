using System.Collections.Generic;

namespace FIFTTOW.Interfaces
{
    public interface IStorageService<T> where T : class
    {
        void Add(T obj);
        List<T> GetAll();
        void Save(IEnumerable<T> items);
    }
}