using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.Domain.Entities;
using User.Domain.ValueObjects;

namespace User.Persistence.Extensions;

public sealed class UserContextSeed
{
    public static async Task SeedAsync(UserManager<UserAccount> userManager, ILogger<UserContextSeed> logger) {
        if ( !await userManager.Users.AnyAsync() ) {
            foreach ( var customer in GetCustomersList() ) {
                var result = await userManager.CreateAsync(customer, "123456");
            }
            logger.LogInformation("Add seed data completed.");
        }
    }

    public static IEnumerable<Customer> GetCustomersList() {
        return new List<Customer>
        {
            new ()
            {
                FirstName = "Ali",
                LastName = "Rezaei",
                BirthDate = new DateTime(1990, 5, 14),
                Email = "ali.rezaei@example.com",
                UserName = "ali.rezaei@example.com",
                PhoneNumber = "09121234567",
                Addresses = [
                    new Address {
                        City = "Tehran",
                        Street = "Valiasr Street",
                        LicensePlateHouse = "12",
                        Location = new GeoLocation(35.7324, 51.4220)
                    }
                ]
            },
            new ()
            {
                FirstName = "Sara",
                LastName = "Mohammadi",
                BirthDate = new DateTime(1995, 11, 2),
                Email = "ali.rezaei@example.com",
                UserName = "ali.rezaei@example.com",
                PhoneNumber = "09301234567",
                Addresses = [
                    new Address {
                        City = "Isfahan",
                        Street = "Chaharbagh Abbasi",
                        LicensePlateHouse = "45",
                        Location = new GeoLocation(32.6546, 51.6680)
                    }
                ]
            },
            new ()
            {
                FirstName = "Reza",
                LastName = "Karimi",
                BirthDate = new DateTime(1988, 3, 22),
                Email = "ali.rezaei@example.com",
                UserName = "ali.rezaei@example.com",
                PhoneNumber = "09381234567",
                Addresses = [
                    new Address {
                        City = "Shiraz",
                        Street = "Zand Street",
                        LicensePlateHouse = "7B",
                        Location = new GeoLocation(29.5918, 52.5837)
                    }
                ]
            },
        };
    }
}
