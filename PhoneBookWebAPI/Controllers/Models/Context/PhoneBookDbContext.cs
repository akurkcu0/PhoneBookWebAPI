using Microsoft.EntityFrameworkCore;

namespace PhoneBookWebAPI.Controllers.Models.Context
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
