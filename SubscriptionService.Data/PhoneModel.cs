namespace SubscriptionService.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PhoneModel : DbContext
    {
        public PhoneModel()
            : base("name=PhoneModel")
        {
        }

        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.VatAmount)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Subscription>()
                .Property(e => e.CallMinutes)
                .HasPrecision(18, 0);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Subscriptions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
