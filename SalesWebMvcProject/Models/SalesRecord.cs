using SalesWebMvcProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvcProject.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy)}")]
        public DateTime Date { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Amount { get; set; }
        public string Title { get; set; }
        public SaleStatus Status { get; set; }
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public SalesRecord()
        { }
        public SalesRecord(int id, DateTime date, double amount, string title, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Title = title;
            Status = status;
            Seller = seller;
        }
    }
}
