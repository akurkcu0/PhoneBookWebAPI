using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Models.Context;
using PhoneBookWebAPI.Models.Entities;

namespace PhoneBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInformationsController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;

        public ContactInformationsController(PhoneBookDbContext phoneBookDbContext)
        {
            _context= phoneBookDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContactInformation contactInformation)
        {
            await _context.contactInformations.AddAsync(contactInformation);
            await _context.SaveChangesAsync();

            PhoneBook phoneBook = await _context.phoneBooks.Include(p=> p.contactInformations).FirstAsync(p=> p.Id==contactInformation.PhoneBookId);

            return Ok(phoneBook);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            ContactInformation contactInformation = await _context.contactInformations.FindAsync(id);
            _context.Remove(contactInformation);
            await _context.SaveChangesAsync();

            PhoneBook phoneBook = await _context.phoneBooks.Include(p => p.contactInformations).FirstAsync(p => p.Id == contactInformation.PhoneBookId);

            return Ok(phoneBook);
        }
    }
}
