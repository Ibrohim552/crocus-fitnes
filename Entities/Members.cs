namespace CrocusFitnes.Entities;

public class Members:PersonBase
{
    public int MembershipId { get; set; }
    public DateTime DateOfBirth { get; set; } 
    public string? Status { get; set; } 
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}