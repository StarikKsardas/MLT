using MLT.Data.Contracts.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Data.Contracts.Repositories
{
    public interface IInformationRepository
    {
        IEnumerable<Atd> GetAllAtds();
        IEnumerable<AbrPlace> GetAllAbrPlaces();
        IEnumerable<EntrancePlace> GetAllEntrancePlaces();
        IEnumerable<EntranceType> GetAllEntranceTypes();
        IEnumerable<CrimeType> GetAllCrimeTypes();
        int GetDactoMobileUserNumber();
        int GetDactoMobileUserNumber(bool isAcceptor);
        int GetSequanceNextVal(string sequanceName);
        int GetSequanceNextVal(string sequanceName, bool isAcceptor);
        void DeatachAllEntities();
    }
}
