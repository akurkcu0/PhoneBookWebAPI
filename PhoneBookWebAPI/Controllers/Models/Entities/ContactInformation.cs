namespace PhoneBookWebAPI.Controllers.Models.Entities
{
    public class ContactInformation
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
        public int PhoneBookId { get; set; }
    }
}
