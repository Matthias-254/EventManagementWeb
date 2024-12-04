using EventManagementWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManagementWeb.Data
{
    public class SeedDataContext
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<EventManagementUser> userManager)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            EventManagementUser dummyUser = null;
            EventManagementUser testUser = null;

            if(!context.Languages.Any())
            {
                context.Languages.AddRange(
                    new Language(),
                    new Language { Code = "en", IsSystemLanguage = true, Name = "English" },
                    new Language { Code = "nl", IsSystemLanguage = true, Name = "Nederlands" },
                    new Language { Code = "fr", IsSystemLanguage = true, Name = "Français" },
                    new Language { Code = "de", IsSystemLanguage = false, Name = "Deutsch" }
                    );
                context.SaveChanges();
            }

            Language.Languages = context.Languages.Where(l => l.IsSystemLanguage && l.Code != "?").ToList();

            if(context.Users.FirstOrDefault(u => u.Id == "?") == null)
            {
                dummyUser = new EventManagementUser { Id = "?", UserName = "?", FirstName = "?", LastName = "?", Email = "?@?", PasswordHash = "?", LockoutEnabled = true, LanguageCode = "?" };
                testUser = new EventManagementUser { UserName = "Test", FirstName = "Test", LastName = "Test", Email = "Test@Test.be", LanguageCode = "?" };
                context.Users.Add(dummyUser);
                context.SaveChanges();
                var result = await userManager.CreateAsync(testUser, "Xxx!12345");
            }

            dummyUser = context.Users.FirstOrDefault(u => u.UserName == "?");
            testUser = context.Users.FirstOrDefault(u => u.UserName == "Test");
            

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" },
                    new IdentityRole { Id = "UserAdmin", Name = "UserAdmin", NormalizedName = "USERADMIN" }
                    );
                context.SaveChanges();
                context.UserRoles.Add(new IdentityUserRole<string> { RoleId = "User", UserId = "?" });
                context.SaveChanges();
            }


            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new Location { Name = "?", Address = "?", Description = "?", Deleted = DateTime.Now },
                    new Location { Name = "Kasteel van Huizingen", Address = "Henry Torleylaan 100, 1654 Huizingen", Description = "Huizingen Castle is a castle with domain in the municipality of Beersel, belonging to the Flemish Brabant." },
                    new Location { Name = "Le Grand Salon", Address = "Herman Teirlinckplein 4, 1650 Beersel", Description = "A new event hall with a surprise effect for your guests." },
                    new Location { Name = "Feestzaal Ter Heyde", Address = "Kesterweg 35, 1755 Gooik", Description = "Planning for your business, introducing a new product to your customers or building your team." }
                );
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                Location location = context.Locations.FirstOrDefault(l => l.Name == "?");
                context.Events.AddRange(
                    new Event { Name = "?", Description = "?", Deleted = DateTime.Now, LocationId = location.Id },
                    new Event { Name = "Wine tasting Evening", Description = "New local wine tasting time.", LocationId = 1 },
                    new Event { Name = "Kunsttentoonstelling", Description = "Art exhibition of all kinds of artists from the region", LocationId = 2 },
                    new Event { Name = "Boekenbeurs", Description = "Book fair of various books made by authors from the region", LocationId = 3 }
                    );
                context.SaveChanges();
            }
        }
    }
}
