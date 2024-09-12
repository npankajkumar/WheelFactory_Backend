﻿namespace WheelFactory.Models
{
    public class PackDTO
    {
        public int OrderId { get; set; }
        public int IRating {  get; set; }
        public string ImageUrl { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow.AddHours(5).AddMinutes(30);
    }
}
