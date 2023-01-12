using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBookWebAPI.Models.Context;

namespace PhoneBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;

        public ReportsController(PhoneBookDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var locations = _context.contactInformations.GroupBy(p => p.Location).Select(s => s.Key).ToList();
            var result = from location in locations
                         select new
                         {
                             Location = location,
                             NumberOfPeople = _context.contactInformations.Where(p => p.Location == location).Count(),
                             NumberOfPhone = _context.contactInformations.Where(p => p.Location == location && p.PhoneNumber != "").Count(),
                         };

            return Ok(result.OrderBy(p => p.NumberOfPeople).ToList());
        }
    }
}
