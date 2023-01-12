using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Models.Context;
using PhoneBookWebAPI.Models.Dtos;
using PhoneBookWebAPI.Models.Entities;

namespace PhoneBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBooksController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;

        public PhoneBooksController(PhoneBookDbContext phoneBookDbContext)
        {
            _context = phoneBookDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PhoneBookDto phoneBookDto)
        {
            PhoneBook phoneBook = _mapper.Map<PhoneBook>(phoneBookDto);

            await _context.phoneBooks.AddAsync(phoneBook);
            await _context.SaveChangesAsync();

            return Ok("Kayıt işlemi başarılı");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(int id ,PhoneBookDto phoneBookDto)
        {
            PhoneBook phoneBook = await _context.phoneBooks.FindAsync(id);
            phoneBook.Name= phoneBookDto.Name;
            phoneBook.Lastname= phoneBookDto.Lastname;
            phoneBook.CompanyName= phoneBookDto.CompanyName;

            await _context.SaveChangesAsync();

            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            PhoneBook phoneBook = await _context.phoneBooks.FindAsync(id);

            _context.Remove(phoneBook);
            await _context.SaveChangesAsync();

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<PhoneBook> phoneBook = await _context.phoneBooks.Include(p=> p.contactInformations).ToListAsync();

            return Ok(phoneBook);
        }

    }
}
