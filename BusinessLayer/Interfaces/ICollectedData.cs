using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICollectedData<in TKey, out TData, in TBaseData>
    {
        IEnumerable<TData> GetAll();

        TData GetById(TKey id);

        // IEnumerable<TData> GetAll();
        void Create(TBaseData entity);
        void Delete(TKey id);
        bool IsExist(TKey id);
        void SaveChanges();
    }
}