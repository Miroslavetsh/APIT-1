using System;
using DatabaseLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ITopicsRepository : ICollectedData<Guid, Topic, Topic>
    {
        bool IsExist(string name);
        Topic GetByName(string name);
    }
}