using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookWebAPI.Models.Context;
using PhoneBookWebAPI.Models.Dtos;
using PhoneBookWebAPI.Models.Entities;
using ServiceStack.Redis.Generic;
using ServiceStack.Redis;
using System;

namespace PhoneBookWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBooksController : ControllerBase
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;

        public PhoneBooksController(PhoneBookDbContext phoneBookDbContext, IMapper mapper)
        {
            _context = phoneBookDbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(PhoneBookDto phoneBookDto)
        {
            if (phoneBookDto == null) return BadRequest();

            PhoneBook phoneBook = _mapper.Map<PhoneBook>(phoneBookDto);

            await _context.phoneBooks.AddAsync(phoneBook);
            await _context.SaveChangesAsync();

            RemoveCache<List<PhoneBook>>("phoneBooks");

            return Ok("Kayıt işlemi başarılı");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(int id ,PhoneBookDto phoneBookDto)
        {
            if (phoneBookDto == null) return BadRequest();

            PhoneBook phoneBook = await _context.phoneBooks.FindAsync(id);
            phoneBook.Name= phoneBookDto.Name;
            phoneBook.Lastname= phoneBookDto.Lastname;
            phoneBook.CompanyName= phoneBookDto.CompanyName;

            await _context.SaveChangesAsync();

            RemoveCache<List<PhoneBook>>("phoneBooks");

            return Ok("Güncelleme işlemi başarılı");
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0) return BadRequest();

            PhoneBook phoneBook = await _context.phoneBooks.FindAsync(id);

            _context.Remove(phoneBook);
            await _context.SaveChangesAsync();

            RemoveCache<List<PhoneBook>>("phoneBooks");

            return Ok("Silme işlemi başarılı");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            List<PhoneBook> phoneBooksList = GetCache<List<PhoneBook>>("phoneBooks");
            if (phoneBooksList == null)
            {
                phoneBooksList = await _context.phoneBooks.Include(p => p.contactInformations).ToListAsync();
                if (phoneBooksList == null) return BadRequest();
                SetCache<List<PhoneBook>>("phoneBooks", phoneBooksList);
            }

            return Ok(phoneBooksList);
        }

        T GetCache<T>(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<List<PhoneBook>> phoneBooks = redisclient.As<List<PhoneBook>>();

            return redisclient.Get<T>(key);
        }

        void SetCache<T>(string key, T value)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<List<PhoneBook>> phoneBooks = redisclient.As<List<PhoneBook>>();

            redisclient.Set<T>(key, value);
        }

        void RemoveCache<T>(string key)
        {
            var redisclient = new RedisClient("localhost", 6379);
            IRedisTypedClient<T> phoneBooks = redisclient.As<T>();

            redisclient.Remove(key);
        }

    }
}
