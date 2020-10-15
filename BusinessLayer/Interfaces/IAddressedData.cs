using System.Collections.Generic;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IAddressedData<out TData>
    {
        string GenerateUniqueAddress();
        TData GetByUniqueAddress(string address);
        IEnumerable<TData> GetLatest(ushort count);
    }
}