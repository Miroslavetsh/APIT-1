using System;
using DatabaseLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ITopicsRepository : ICollectedData<Guid, Topic>
    {
        Topic GetByName(string name);
    }
}