using MLT.Domain.Contracts.InfoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.Services
{
    public interface IInformationService
    {
        IEnumerable<AtdInfo> GetAllAtds();
        IEnumerable<string> GetAllAbrPlaces();
        IEnumerable<EntrancePlaceInfo> GetAllEntrancePlace();
        IEnumerable<EntranceTypeInfo> GetAllEntranceTypes();
        IEnumerable<CrimeTypeInfo> GetAllCrimeTypes();
        void DeatachedAll();
    }
}
