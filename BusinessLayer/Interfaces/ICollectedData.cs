using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICollectedData<TKey, TData>
    {
        IEnumerable<TData> GetAll();

        TData GetById(TKey id);

        // IEnumerable<TData> GetAll();
        void Create(TData entity);
        void Delete(TKey id);
        bool IsExist(TKey id);
        void SaveChanges();
    }
}