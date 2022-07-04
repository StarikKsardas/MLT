using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Repositories;
using MLT.Data.Repositories.DatabaseContext;
using MLT.Data.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLT.Data.Repositories.Repositories
{
    public class InformationRepository : IInformationRepository
    {
        private readonly MLTDbContext dbContext;
        private readonly IServiceProvider serviceProvider;

        public InformationRepository(MLTDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.dbContext = dbContext;
            this.serviceProvider = serviceProvider;
        }

        public void DeatachAllEntities()
        {
            dbContext.DeatachAllEntities();
            var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
            if (dactoDbContext != null)
            {
                dactoDbContext.DeatachAllEntities();
            }
        }

        public IEnumerable<AbrPlace> GetAllAbrPlaces()
        {
            var result = dbContext.AbrPlaces.ToList();
            return result;
        }

        public IEnumerable<Atd> GetAllAtds()
        {
            var result = dbContext.Atds.ToList();
            return result;
        }

        public IEnumerable<CrimeType> GetAllCrimeTypes()
        {
            var result = dbContext.CrimeTypes.ToList();
            return result;
        }

        public IEnumerable<EntrancePlace> GetAllEntrancePlaces()
        {
            var result = dbContext.EntrancePlaces.ToList();
            return result;
        }

        public IEnumerable<EntranceType> GetAllEntranceTypes()
        {
            var result = dbContext.EntranceTypes.ToList();
            return result;
        }

        public int GetDactoMobileUserNumber(bool isAcceptor)
        {
            if (!isAcceptor)
            {
                throw new NotImplementedException();
            }
            var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
            var result = dactoDbContext.DactoUsers.FirstOrDefault(p => p.UsrId == "DACTOMOBILE").UsrNumber;
            return result;
        }

        public int GetDactoMobileUserNumber()
        {
            var result = dbContext.DactoUsers.FirstOrDefault(p => p.UsrId == "DACTOMOBILE").UsrNumber;
            return result;
        }

        public int GetSequanceNextVal(string sequanceName)
        {
            int result = 0;
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT {sequanceName}.NEXTVAL FROM DUAL";
                result = decimal.ToInt32((decimal)command.ExecuteScalar());
            }
            return result;
        }

        public int GetSequanceNextVal(string sequanceName, bool isAcceptor)
        {
            if (!isAcceptor)
            {
                throw new NotImplementedException();
            }
            var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
            int result = 0;
            var connection = dactoDbContext.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"SELECT {sequanceName}.NEXTVAL FROM DUAL";
                result = decimal.ToInt32((decimal)command.ExecuteScalar());
            }
            return result;            
        }
    }
}
