using MLT.Domain.Contracts.InfoModels;
using System.Collections.Generic;

namespace MLT.Domain.Contracts.Services
{
    public interface ILatentService
    {
        void Add(LatentInfo latentInfo);
        IEnumerable<int> GetDifferenceIds();
        void SyncLatent(IEnumerable<int> latentIds);
        IEnumerable<LatentInfo> GetAllUserLatents(UserInfo userInfo);
    }
}
