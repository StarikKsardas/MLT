using System;
using System.Collections.Generic;
using System.Text;

namespace MLT.Domain.Contracts.Services
{
    public interface IQueryService
    {
        IEnumerable<int> IdsToAdd { get; }
        IEnumerable<int> IdsToDelete { get; }
        bool CalculateChanges();
        void DeleteDifference();
        void AddDifference();
    }
}
