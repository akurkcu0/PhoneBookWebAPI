using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Models.Entities;

namespace PhoneBookWebAPI.Models.Context
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<PhoneBook> phoneBooks { get; set; }
        public DbSet<ContactInformation> contactInformations { get; set; }
    }
}
