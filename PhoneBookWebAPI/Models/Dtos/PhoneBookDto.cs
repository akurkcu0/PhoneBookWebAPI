namespace PhoneBookWebAPI.Models.Dtos
{
    public class PhoneBookDto
    {
        //Auto mapper is used to not enter contact information while registering Phone Book
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string CompanyName { get; set; }
    }
}
