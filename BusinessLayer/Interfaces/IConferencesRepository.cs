using System;
using BusinessLayer.Models;

namespace BusinessLayer.Interfaces
{
    public interface IConferencesRepository :
        ICollectedData<Guid, ConferenceViewModel>,
        IAddressedData<ConferenceViewModel>
    {
    }
}