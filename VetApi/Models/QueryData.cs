using System;

namespace VetApi.Models
{
    public class QueryData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Weitgth { get; set; }
        public decimal Temperature { get; set; }
    }
}