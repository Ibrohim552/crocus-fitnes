namespace CrocusFitnes.Entities;

public class Trainers:PersonBase
{
    public string? Specialization { get; set; }
    public double Rating { get; set; } 
    public int ExperienceYears { get; set; }
    public ICollection<Session> Sessions { get; set; } = new List<Session>();

}