using Microsoft.EntityFrameworkCore;
using MLT.Data.Contracts.Entitys;

namespace MLT.Data.Repositories.DatabaseContext
{
    public class MLTDbContext : DbContext
    {
        public MLTDbContext(DbContextOptions<MLTDbContext> options)
           : base(options)
        {           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("WEB_USER_SQ");
            modelBuilder.HasSequence("DACTO_SL_SQ");
            modelBuilder.HasSequence("ANSWER_MOBILE_SQ");           
           
            modelBuilder.Entity<LatentImage>(entity =>
            {
                entity.HasKey(e => new { e.DsId, e.ImageId });
                entity.Property(e => e.Image)
                      .IsRequired()
                      .HasColumnType("LONG RAW")
                      .HasColumnName("IMAGE")
                      .HasMaxLength(5000000);
            });

            modelBuilder.Entity<AnswerMobile>()
                .HasKey(e => new { e.Id });

            modelBuilder.Entity<AnswerMobile>()
                .HasIndex(e => new { e.DsId, e.QueryId }).IsUnique();

            modelBuilder.Entity<AnswerMobile>()
             .HasOne(e => e.Latent)
             .WithMany(p => p.AnswerMobiles)
             .HasForeignKey(t => t.DsId);


            modelBuilder.Entity<AnswerMobile>()
                 .HasOne(e => e.QueryStatus)
                 .WithMany(p => p.AnswerMobile)
                 .HasForeignKey(t => t.LocalStatus);
                 
                   


            modelBuilder.Entity<LatentEntrancePlace>()
                .HasKey(e => new { e.DsId, e.EntrancePlaceId });

            modelBuilder.Entity<LatentEntranceType>()
                .HasKey(e => new { e.DsId, e.EntranceTypeId });

            modelBuilder.Entity<LatentCrimeType>()
                .HasKey(e => new { e.DsId, e.CrimeTypeId });

            modelBuilder.Entity<EntranceType>()
                .HasKey(e => new { e.EntranceTypeLex, e.ParentEntranceTypeLex, e.IsOnlyParent});

            modelBuilder.Entity<CrimeType>()
                .HasKey(e => new { e.CrimeTypeLex, e.ParentCrimeTypeLex, e.IsOnlyParent });

            
            

          
        }


        public DbSet<Atd> Atds { get; set; }
        public DbSet<AbrPlace> AbrPlaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Latent> Latents { get; set; }
        public DbSet<LatentImage> LatentImages { get; set; }
        public DbSet<CrimeType> CrimeTypes { get; set; }
        public DbSet<EntrancePlace> EntrancePlaces { get; set; }
        public DbSet<EntranceType> EntranceTypes { get; set; }
        public DbSet<DactoUser> DactoUsers { get; set; }
        public DbSet<LatentEntrancePlace> LatentEntrancePlaces { get; set; }
        public DbSet<LatentEntranceType> LatentEntranceTypes { get; set; }
        public DbSet<LatentCrimeType> LatentCrimeTypes { get; set; }
        public DbSet<AnswerMobile> AnswerMobiles { get; set; }
        public DbSet<QueryStatus> QueryStatuses { get; set; }
        
    }
}
