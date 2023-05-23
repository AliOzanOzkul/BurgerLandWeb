using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgerWeb.Models
{
    public class HamburgerContext : IdentityDbContext<IdentityUser>
    {
        public HamburgerContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ExtraMaterial> ExtraMaterials { get; set; }
        public DbSet<Hambureger> Hamburegers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ExtraBasket> ExtraBasket { get; set; }
        public DbSet<HamburgerBasket> HamburgerBaskets { get; set; }
        public DbSet<OrderExtra> OrderExtras { get; set; }
        public DbSet<OrderHamburger> OrderHamburgers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>()
     .HasOne(u => u.Basket)
     .WithOne(b => b.User)
     .HasForeignKey<Basket>(b => b.Id);
            base.OnModelCreating(builder);
            SeedData(builder);



            void SeedData(ModelBuilder builder)
            {
                builder.Entity<Hambureger>().HasData(new Hambureger()
                {
                    Id = 1,
                    Name = "Whopper",
                    Price = 100,
                    Description = "Whopper® eti, büyük boy susamlı sandviç ekmeği, salatalık turşusu, ketçap, mayonez, doğranmış marul, domates ve soğandan oluşan bir Burger King® klasiği.",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/whopper.png?v=239"

                },
                new Hambureger()
                {
                    Id = 2,
                    Name = "Rodeo Whopper",
                    Price = 125,
                    Description = "Whopper® eti, büyük boy susamlı sandviç ekmeği, mayonez, doğranmış marul, soğan halkaları, nefis barbekü sosu ve 2 adet cheddar peynirinden oluşan Whopper® lezzeti.",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/rodeo-whopper.png?v=239"

                },
                new Hambureger()
                {
                    Id = 3,
                    Name = "Cheeseburger",
                    Price = 80,
                    Description = "Hamburger eti, küçük boy susamlı sandviç ekmeği, salatalık turşusu, cheddar peyniri, hardal ve ketçaptan oluşan lezzet.",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/cheeseburger.png?v=239"

                },
                new Hambureger()
                {
                    Id = 4,
                    Name = "Chicken Royale®",
                    Price = 85,
                    Description = "Chicken Royale® eti, uzun ve büyük boy susamlı sandviç ekmeği, mayonez ve doğranmış marul lezzeti.",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/chicken-royale.png?v=239"

                },
                new Hambureger()
                {
                    Id = 5,
                    Name = "Spicy Gurme Tavuk",
                    Price = 90,
                    Description = "Özel kaplamasıyla tavuk göğüs fileto, mısır irmiği ile süslemeli özel ekmeği, spicy sosu, domatesi ve doğranmış maruldan oluşan yeni bir Burger King lezzeti.",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/rodeo-whopper.png?v=239"

                }
                );

                builder.Entity<ExtraMaterial>().HasData(new ExtraMaterial()
                {
                    Id = 1,
                    Name = "Ekstra Malzeme Eklemek İçin Tıklayınız...",

                    Price = 0

                }, new ExtraMaterial()
                {
                    Id = 2,
                    Name = "Mini Acı Sos",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/mini-aci-sos.png?v=239",
                    Price = 3

                },
                new ExtraMaterial()
                {
                    Id = 3,
                    Name = "Mini Mayonez",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/mini-mayonez.png?v=239",
                    Price = 2
                },
                new ExtraMaterial()
                {
                    Id = 4,
                    Name = "Mini BBQ",
                    PictureUrl = "https://www.burgerking.com.tr/cmsfiles/products/mini-bbq.png?v=239",
                    Price = 4
                }
                );

                builder.Entity<AppUser>().HasData(new AppUser()
                {
                    Id = "1",
                    UserName = "Test",
                    Email = "Test"


                });
                builder.Entity<IdentityRole>().HasData(new IdentityRole()
                {
                    Name = "Admin",
                    Id = "2",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole()
                {
                    Name = "User",
                    Id = "1",
                    NormalizedName = "USER"
                });
                //Roll Seed

            }
        }
    }
}
