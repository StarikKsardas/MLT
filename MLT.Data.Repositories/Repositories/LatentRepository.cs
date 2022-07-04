using Microsoft.EntityFrameworkCore;
using MLT.Data.Contracts.Entitys;
using MLT.Data.Contracts.Helpers;
using MLT.Data.Contracts.Repositories;
using MLT.Data.Repositories.DatabaseContext;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLT.Data.Repositories.Repositories
{
    public class LatentRepository : ILatentRepository
    {
        private readonly MLTDbContext mLTDbContext;
        private readonly IServiceProvider serviceProvider;
        private const int MaxSize = 32 * 1024 - 1;

        public LatentRepository(MLTDbContext mLTDbContext, IServiceProvider serviceProvider)
        {
            this.mLTDbContext = mLTDbContext;
            this.serviceProvider = serviceProvider;
        }

        // In DB use long raw - ef core max size for long raw 32k
        private void AddImageMore32k(LatentImage latentImage, DbContext dbContext)
        {
            var connection = dbContext.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            var command = new OracleCommand()
            {
                InitialLONGFetchSize = -1,
                Connection = (OracleConnection)connection,
                CommandText = "insert into dactocreator.dacto_image_sl (ds_id, image_id, image) "
                + "values (:ds_id, :image_id, :image)",
                CommandType = System.Data.CommandType.Text
            };
            command.Parameters.Add(new OracleParameter()
            {
                Value = latentImage.DsId,                
            });
            command.Parameters.Add(new OracleParameter()
            {
                Value = latentImage.ImageId,               
            });
            command.Parameters.Add(new OracleParameter()
            {
                Value = latentImage.Image,                                
            });            
            command.ExecuteNonQuery();            
        }

        public void Add(Latent latent)
        {            
            latent.CrimeDate = latent.CrimeDate?.Date;
            LatentImage latentImage = null;
            if (latent.LatentImages.FirstOrDefault().Image.Length > MaxSize)
            {
                latentImage = ((List<LatentImage>)latent.LatentImages)[0];
                latent.LatentImages = null;
            }
            mLTDbContext.Latents.Add(latent);
            mLTDbContext.SaveChanges();
            if (latentImage != null)
            {
                AddImageMore32k(latentImage, mLTDbContext);
            }                
        }

        public void Add(Latent latentDactoDb, bool isAcceptor)
        {
            if (!isAcceptor)
            {
                throw new NotImplementedException();
            }
            var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
            latentDactoDb.CrimeDate = latentDactoDb.CrimeDate?.Date;
            LatentImage latentImage = null;
            if (latentDactoDb.LatentImages.FirstOrDefault().Image.Length > MaxSize)
            {
                latentImage = latentDactoDb.LatentImages.FirstOrDefault();
                latentDactoDb.LatentImages = null;
            }
            dactoDbContext.Latents.Add(latentDactoDb);
            dactoDbContext.SaveChanges();
            if (latentImage != null)
            {
                AddImageMore32k(latentImage, dactoDbContext);
            }
        }

        public Latent GetById(int id)
        {
            var result = mLTDbContext.Latents.FirstOrDefault(p => p.DsId == id);
            return result;
        }

        public IEnumerable<LatentUnique> GetUniqueIdentifiers(bool isAcceptor)
        {
            IEnumerable<LatentUnique> result;
            if (isAcceptor)
            {
                var dactoDbContext = (DactoDbContext)serviceProvider.GetService(typeof(DactoDbContext));
                result = dactoDbContext.Latents.Select(p => new LatentUnique(p.DsId, p.SDsId, p.SUsrId, p.SPlaceCode, p.SAbrPlace, p.SEditDate));
            }
            else
            {
                result = mLTDbContext.Latents.Select(p => new LatentUnique(p.DsId, p.SDsId, p.SUsrId, p.SPlaceCode, p.SAbrPlace, p.SEditDate));                
            }
            return result;
        }

        public IEnumerable<Latent> GetAllByUserNameAndId(string userName, int userId)
        {
            var result = mLTDbContext.Latents.Where(p => p.SUsrId == $"{userName}{userId}");
            return result;
        }
    }
}
