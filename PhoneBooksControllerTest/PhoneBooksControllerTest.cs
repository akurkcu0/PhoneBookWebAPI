using AutoMapper;
using FakeItEasy;
using PhoneBookWebAPI.Controllers;
using PhoneBookWebAPI.Models.Context;
using PhoneBookWebAPI.Models.Dtos;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PhoneBookWebAPITest
{
    public class PhoneBooksControllerTest
    {
        private readonly PhoneBookDbContext _context;
        private readonly IMapper _mapper;
        public PhoneBooksControllerTest()
        {
            _context = A.Fake<PhoneBookDbContext>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public async void PhoneBooksController_GetAllPhoneBooks_ReturnOk()
        {
            //Arrange

            var controller = new PhoneBooksController(_context, _mapper);

            //Act

            var result = await controller.Get();

            //Assert

            Assert.IsNotNull(result);
        }

        [Fact]
        public async void PhoneBooksController_CreatePhoneBook_ReturnOk()
        {
            //Arrange

            var phoneBookDto = new PhoneBookDto { CompanyName= "Test", Lastname= "test2", Name="tes1" };
            var controller = new PhoneBooksController(_context, _mapper);

            //Act
            var result = await controller.Post(phoneBookDto);

            //Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async void PhoneBooksController_PutPhoneBook_ReturnOk()
        {
            //Arrange
            int id = 1;
            var phoneBookDto = new PhoneBookDto { CompanyName = "Test", Lastname = "test2", Name = "tes1" };
            var controller = new PhoneBooksController(_context, _mapper);

            //Act
            var result = await controller.Put(id, phoneBookDto);

            //Assert
            Assert.IsNotNull(result);
        }

        [Fact]
        public async void PhoneBooksController_DeletePhoneBook_ReturnOk()
        {
            //Arrange
            int id = 1;
            var controller = new PhoneBooksController(_context, _mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
