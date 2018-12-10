using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeduNetcore.Data.EF.Configurations;
using TeduNetcore.Data.EF.Extensions;
using TeduNetcore.Data.Entities;
using TeduNetcore.Data.Interfaces;

namespace TeduNetcore.Data.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Language> Languages { set; get; }
        public DbSet<SystemConfig> SystemConfigs { get; set; }
        public DbSet<Function> Functions { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        public DbSet<Blog> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }

        public DbSet<Size> Sizes { set; get; }
        public DbSet<Slide> Slides { set; get; }

        public DbSet<Tag> Tags { set; get; }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<WholePrice> WholePrices { get; set; }

        public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
        public DbSet<Advertistment> Advertistments { get; set; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Tag>(entity => entity.Property<string>("Id").HasMaxLength(50)
            //                          .IsRequired(true)
            //                          .HasColumnType("VarChar50"));
            builder.Entity<AdvertistmentPage>(entity => entity.Property(c => c.Id).HasMaxLength(20)
                                                                                     .IsRequired()
                                                                                       .HasColumnType("nvarchar(20)"));
            builder.AddConfiguration(new TagConfiguration());
            builder.AddConfiguration(new BlogTagConfiguration());
            builder.AddConfiguration(new ContactDetailConfiguration());

            builder.AddConfiguration(new PageConfiguration());
            builder.AddConfiguration(new FooterConfiguration());

            builder.AddConfiguration(new ProductTagConfiguration());
            builder.AddConfiguration(new SystemConfigConfiguration());
            builder.AddConfiguration(new AdvertistmentPositionConfiguration());


            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> modified = ChangeTracker.Entries().Where(e => e.State.Equals(EntityState.Modified | EntityState.Added));
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry item in modified)
            {
                IDateTracking updateOrAddItem = item.Entity as IDateTracking;
                if (updateOrAddItem != null)
                {
                    if (item.State.Equals(EntityState.Added))
                    {
                        updateOrAddItem.DateCreated = DateTime.Now;
                    }
                    updateOrAddItem.DateModified = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                     .AddJsonFile("appsettings.json")
                                                                     .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseSqlServer(connectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
