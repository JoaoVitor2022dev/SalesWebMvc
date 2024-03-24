namespace SalesWebMvcProject.Models.ViewModels
{
    public class SalesRecordsFormViewModel
    {
        public SalesRecord salesRecord { get; set; }
        public ICollection<Seller> Sellers { get; set; }
    }
}
