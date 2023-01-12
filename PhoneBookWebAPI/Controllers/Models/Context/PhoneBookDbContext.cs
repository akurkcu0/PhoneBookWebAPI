using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Controllers.Models.Entities;

namespace PhoneBookWebAPI.Controllers.Models.Context
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
