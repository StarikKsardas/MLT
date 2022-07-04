using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MLT.Data.Contracts.Repositories
{
    public interface ILatentRepository
    {
        void Add(Latent latent);
        void Add(Latent latentDactoDb, bool isAcceptor);
        Latent GetById(int id);
        IEnumerable<LatentUnique> GetUniqueIdentifiers(bool isAcceptor);
        IEnumerable<Latent> GetAllByUserNameAndId(string userName, int userId);
    }
}
