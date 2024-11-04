using CrocusFitnes.Dto_s;

namespace CrocusFitnes.Entities;

public class Booking : BaseEntity
{
    public string Status { get; set; }
    public int MemberId { get; set; }
        public Members Members { get; set; }

        public int SessionId { get; set; }
        public Session Session { get; set; }

   
}