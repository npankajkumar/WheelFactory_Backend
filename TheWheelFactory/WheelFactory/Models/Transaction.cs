namespace WheelFactory.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public DateTime? Created { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);



    }
}
