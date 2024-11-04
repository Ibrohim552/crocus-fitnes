namespace CrocusFitnes.Entities;

public class Session : BaseEntity
{
    public string? Name { get; set; } 
    public int TrainerId { get; set; }
    public Trainers Trainers { get; set; }
    public string? Location { get; set; } 
    public int MaxParticipants { get; set; }
    public DateTime Duration { get; set; }
    public DateTime StartTime { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

}