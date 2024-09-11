
namespace WheelFactory.Models
{
    public class OrderDTO
    {
        
     
           
            public string ClientName { get; set; }
            public int Year { get; set; }
            public int Make { get; set; }
            public int Model { get; set; }
            public string DamageType { get; set; }
            public string ImageUrl { get; set; }
            public string Notes { get; set; }
            public string Status { get; set; }
            public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    }
    }
