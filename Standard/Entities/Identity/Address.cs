namespace Standard.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Building { get; set; }
        public string? Apartment { get; set; }

        //Below is Relationsgip between AppUser and Address
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}