using Microsoft.EntityFrameworkCore;
using PROWeb.Data.Models;

namespace PROWeb.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Voter> Voters => Set<Voter>();

        public DbSet<Assessment> Assessments => Set<Assessment>();

        public DbSet<Country> Countries => Set<Country>();

        public DbSet<Constituency> Constituencies => Set<Constituency>();

        public DbSet<Parish> Parishes => Set<Parish>();

        public DbSet<VoterFlag> VoterFlags => Set<VoterFlag>();

        public DbSet<Registration> Registrations => Set<Registration>();

        public DbSet<RegistrationStatus> RegistrationStatuses => Set<RegistrationStatus>();

        public DbSet<RegistrationOrigin> RegistrationOrigins => Set<RegistrationOrigin>();

        public DbSet<FormType> FormTypes => Set<FormType>();

        public DbSet<Immigration> Immigrations => Set<Immigration>();

        public DbSet<Birth> Births => Set<Birth>();

        public DbSet<DriverLicense> DriverLicenses => Set<DriverLicense>();

        public DbSet<Document> Documents => Set<Document>();

        public DbSet<PROOffice> PROOffices => Set<PROOffice>();

        public DbSet<OldPROUser> OldPROUsers => Set<OldPROUser>();

        public DbSet<AssessmentFlag> AssessmentFlags => Set<AssessmentFlag>();

        public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();


        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(options =>
            {
                // See https://go.microsoft.com/fwlink/?linkid=2134277, https://github.com/dotnet/efcore/issues/22580 for more information.
                options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
            });
            //.EnableSensitiveDataLogging()
            //.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Voter>().HasKey(v => new { v.VoterId, v.RegistryYear });

            modelBuilder.Entity<VoterFlag>()
                .HasMany<Voter>(v => v.Voters)
                .WithMany(vf => vf.Flags)
                .UsingEntity<Dictionary<string, object>>(
                "VoterVoterFlag",
                    j => j.HasOne<Voter>()
                    .WithMany()
                    .HasForeignKey("VoterId", "RegistryYear")
                    .HasConstraintName("FK_VoterVoterFlag_Voters_VoterId_RegistryYear")
                    .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<VoterFlag>()
                    .WithMany()
                    .HasForeignKey("FlagId")
                    .HasConstraintName("FK_VoterVoterFlag_VoterFlags_FlagId")
                    .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<Registration>()
            .HasOne(r => r.OldAssessment)
            .WithMany(a => a.OldRegistrations)
            .HasForeignKey(r => r.OldAssessmentNo)
            .HasPrincipalKey(a => a.AssessmentNo)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Voter>()
            .HasOne(v => v.BogusConstituency)
            .WithMany(c => c.Voters)
            .HasForeignKey(v => v.BogusNo)
            .HasPrincipalKey(c => c.BogusNo)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
            .HasOne(r => r.BogusConstituency)
            .WithMany(c => c.Registrations)
            .HasForeignKey(r => r.BogusNo)
            .HasPrincipalKey(c => c.BogusNo)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
            .HasOne(r => r.OldBogusConstituency)
            .WithMany(c => c.OldRegistrations)
            .HasForeignKey(r => r.OldBogusNo)
            .HasPrincipalKey(c => c.BogusNo)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
            .HasOne(r => r.Voter)
            .WithMany(v => v.Registrations)
            .HasForeignKey(r => new { r.VoterId, r.RegistryYear })
            .HasPrincipalKey(v => new { v.VoterId, v.RegistryYear })
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Document>()
            .HasOne(r => r.Voter)
            .WithMany(v => v.Documents)
            .HasForeignKey(r => new { r.PersonId, r.RegistryYear })
            .HasPrincipalKey(v => new { v.VoterId, v.RegistryYear })
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
