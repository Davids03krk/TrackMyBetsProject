using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TrackMyBets.Data.Models
{
    public partial class BD_TRACKMYBETSContext : DbContext
    {
        public virtual DbSet<Bet> Bet { get; set; }
        public virtual DbSet<Bookmaker> Bookmaker { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Pick> Pick { get; set; }
        public virtual DbSet<PickType> PickType { get; set; }
        public virtual DbSet<RelUserBookmaker> RelUserBookmaker { get; set; }
        public virtual DbSet<Sport> Sport { get; set; }
        public virtual DbSet<StatusType> StatusType { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<TypePickTotalPoints> TypePickTotalPoints { get; set; }
        public virtual DbSet<TypePickWinner> TypePickWinner { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Withdrawal> Withdrawal { get; set; }

        public BD_TRACKMYBETSContext(DbContextOptions<BD_TRACKMYBETSContext> options) 
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bet>(entity =>
            {
                entity.HasKey(e => e.IdBet)
                    .HasName("PK_Bet_IdBet");

                entity.Property(e => e.IdBet).HasColumnName("Id_Bet");

                entity.Property(e => e.Benefits)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.DateBet)
                    .HasColumnName("Date_Bet")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdRelUserBookmaker).HasColumnName("Id_RelUserBookmaker");

                entity.Property(e => e.IdStatusType)
                    .HasColumnName("Id_StatusType")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IsCombinedBet).HasDefaultValueSql("0");

                entity.Property(e => e.IsLiveBet).HasDefaultValueSql("0");

                entity.Property(e => e.Profits)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Stake).HasColumnType("money");

                entity.HasOne(d => d.IdRelUserBookmakerNavigation)
                    .WithMany(p => p.Bet)
                    .HasForeignKey(d => d.IdRelUserBookmaker)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bet_RelUserBookmaker");

                entity.HasOne(d => d.IdStatusTypeNavigation)
                    .WithMany(p => p.Bet)
                    .HasForeignKey(d => d.IdStatusType)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Bet_StatusType");
            });

            modelBuilder.Entity<Bookmaker>(entity =>
            {
                entity.HasKey(e => e.IdBookmaker)
                    .HasName("PK_Bookmaker_IdBookmaker");

                entity.Property(e => e.IdBookmaker).HasColumnName("Id_Bookmaker");

                entity.Property(e => e.DescBookmaker)
                    .IsRequired()
                    .HasColumnName("Desc_Bookmaker")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent)
                    .HasName("PK_Event_IdEvent");

                entity.Property(e => e.IdEvent).HasColumnName("Id_Event");

                entity.Property(e => e.Comment).HasMaxLength(350);

                entity.Property(e => e.DateEvent)
                    .HasColumnName("Date_Event")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdLocalTeam).HasColumnName("Id_LocalTeam");

                entity.Property(e => e.IdSport).HasColumnName("Id_Sport");

                entity.Property(e => e.IdVisitTeam).HasColumnName("Id_VisitTeam");

                entity.HasOne(d => d.IdLocalTeamNavigation)
                    .WithMany(p => p.EventIdLocalTeamNavigation)
                    .HasForeignKey(d => d.IdLocalTeam)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Event_TeamLocal");

                entity.HasOne(d => d.IdSportNavigation)
                    .WithMany(p => p.Event)
                    .HasForeignKey(d => d.IdSport)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Event_Sport");

                entity.HasOne(d => d.IdVisitTeamNavigation)
                    .WithMany(p => p.EventIdVisitTeamNavigation)
                    .HasForeignKey(d => d.IdVisitTeam)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Events_TeamVisit");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.HasKey(e => e.IdIncome)
                    .HasName("PK_Income_IdIncome");

                entity.Property(e => e.IdIncome).HasColumnName("Id_Income");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(350);

                entity.Property(e => e.DateIncome)
                    .HasColumnName("Date_Income")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdRelUserBookmaker).HasColumnName("Id_RelUserBookmaker");

                entity.Property(e => e.IsFreeBonus).HasDefaultValueSql("0");

                entity.HasOne(d => d.IdRelUserBookmakerNavigation)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.IdRelUserBookmaker)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Income_RelUserBookmaker");
            });

            modelBuilder.Entity<Pick>(entity =>
            {
                entity.HasKey(e => e.IdPick)
                    .HasName("PK_Pick_IdPick");

                entity.Property(e => e.IdPick).HasColumnName("Id_Pick");

                entity.Property(e => e.IdBet).HasColumnName("Id_Bet");

                entity.Property(e => e.IdEvent).HasColumnName("Id_Event");

                entity.Property(e => e.IdPickType).HasColumnName("Id_PickType");

                entity.HasOne(d => d.IdBetNavigation)
                    .WithMany(p => p.Pick)
                    .HasForeignKey(d => d.IdBet)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pick_Bet");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.Pick)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pick_Event");

                entity.HasOne(d => d.IdPickTypeNavigation)
                    .WithMany(p => p.Pick)
                    .HasForeignKey(d => d.IdPickType)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Pick_PickType");
            });

            modelBuilder.Entity<PickType>(entity =>
            {
                entity.HasKey(e => e.IdPickType)
                    .HasName("PK_PickType_IdPickType");

                entity.Property(e => e.IdPickType).HasColumnName("Id_PickType");

                entity.Property(e => e.DescPickType)
                    .IsRequired()
                    .HasColumnName("Desc_PickType")
                    .HasMaxLength(75);
            });

            modelBuilder.Entity<RelUserBookmaker>(entity =>
            {
                entity.HasKey(e => e.IdRelUserBookmaker)
                    .HasName("PK_RelUserBookmaker_IdRelUserBookmaker");

                entity.Property(e => e.IdRelUserBookmaker).HasColumnName("Id_RelUserBookmaker");

                entity.Property(e => e.Bankroll)
                    .HasColumnType("money")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IdBookmaker).HasColumnName("Id_Bookmaker");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.HasOne(d => d.IdBookmakerNavigation)
                    .WithMany(p => p.RelUserBookmaker)
                    .HasForeignKey(d => d.IdBookmaker)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelUserBookmaker_Bookmaker");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.RelUserBookmaker)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_RelUserBookmaker_User");
            });

            modelBuilder.Entity<Sport>(entity =>
            {
                entity.HasKey(e => e.IdSport)
                    .HasName("PK_Sport_IdSport");

                entity.Property(e => e.IdSport).HasColumnName("Id_Sport");

                entity.Property(e => e.DescSport)
                    .IsRequired()
                    .HasColumnName("Desc_Sport")
                    .HasMaxLength(75);

                entity.Property(e => e.DurationMatchInHours).HasDefaultValueSql("2.5");
            });

            modelBuilder.Entity<StatusType>(entity =>
            {
                entity.HasKey(e => e.IdStatusType)
                    .HasName("PK_StatusType_IdStatusType");

                entity.Property(e => e.IdStatusType).HasColumnName("Id_StatusType");

                entity.Property(e => e.DescStatusType)
                    .IsRequired()
                    .HasColumnName("Desc_StatusType")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.IdTeam)
                    .HasName("PK_Team_IdTeam");

                entity.Property(e => e.IdTeam).HasColumnName("Id_Team");

                entity.Property(e => e.Abbreviation)
                    .IsRequired()
                    .HasColumnType("nchar(3)");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DescTeam)
                    .HasColumnName("Desc_Team")
                    .HasMaxLength(125);

                entity.Property(e => e.IdSport).HasColumnName("Id_Sport");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Stadium).HasMaxLength(125);

                entity.HasOne(d => d.IdSportNavigation)
                    .WithMany(p => p.Team)
                    .HasForeignKey(d => d.IdSport)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Team_Sport");
            });

            modelBuilder.Entity<TypePickTotalPoints>(entity =>
            {
                entity.HasKey(e => e.IdPick)
                    .HasName("PK_TypePickTotalPoints_IdPick");

                entity.Property(e => e.IdPick)
                    .HasColumnName("Id_Pick")
                    .ValueGeneratedNever();

                entity.Property(e => e.IsOver).HasDefaultValueSql("0");

                entity.Property(e => e.IsUnder).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<TypePickWinner>(entity =>
            {
                entity.HasKey(e => e.IdPick)
                    .HasName("PK_TypePickWinner_IdPick");

                entity.Property(e => e.IdPick)
                    .HasColumnName("Id_Pick")
                    .ValueGeneratedNever();

                entity.Property(e => e.HasHandicap).HasDefaultValueSql("0");

                entity.Property(e => e.IdWinnerTeam).HasColumnName("Id_WinnerTeam");

                entity.Property(e => e.ValueHandicap).HasDefaultValueSql("0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_User_IdUser");

                entity.Property(e => e.IdUser).HasColumnName("Id_User");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.SurnameFirst).HasMaxLength(100);

                entity.Property(e => e.SurnameSecond).HasMaxLength(100);

                entity.Property(e => e.Phone).HasColumnType("nchar(9)");

                entity.Property(e => e.PasswordHash).HasColumnType("varbinary(100)");

                entity.Property(e => e.PasswordSalt).HasColumnType("varbinary(100)");
            });

            modelBuilder.Entity<Withdrawal>(entity =>
            {
                entity.HasKey(e => e.IdWithdrawal)
                    .HasName("PK_Withdrawal_IdWithdrawal");

                entity.Property(e => e.IdWithdrawal).HasColumnName("Id_Withdrawal");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Comment).HasMaxLength(350);

                entity.Property(e => e.DateWithdrawal)
                    .HasColumnName("Date_Withdrawal")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdRelUserBookmaker).HasColumnName("Id_RelUserBookmaker");

                entity.HasOne(d => d.IdRelUserBookmakerNavigation)
                    .WithMany(p => p.Withdrawal)
                    .HasForeignKey(d => d.IdRelUserBookmaker)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_Withdrawal_RelUserBookmaker");
            });
        }
    }
}