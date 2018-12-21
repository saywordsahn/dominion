using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DominionWeb.Models
{
    public partial class DominionContext : IdentityDbContext<ApplicationUser>
    {
        public DominionContext()
        {
        }

        public DominionContext(DbContextOptions<DominionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connection> Connection { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameState> GameState { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Connection>(entity =>
            {
                entity.ToTable("Connection", "Dominion");

                entity.HasIndex(e => e.UserId)
                    .HasName("Idx_Connection_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Connection)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_Connection_User");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game", "Dominion");

                entity.Property(e => e.DateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<GameState>(entity =>
            {
                entity.ToTable("GameState", "Dominion");

                entity.HasIndex(e => e.GameId)
                    .HasName("Idx_GameState_GameId");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.GameState)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_gamestate_game");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player", "Dominion");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Dominion");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
