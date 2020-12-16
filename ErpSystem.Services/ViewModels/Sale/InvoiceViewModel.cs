namespace ErpSystem.Services.ViewModels.Sale
{
    public class InvoiceViewModel : SalesPerCustomerOrProductViewModel
    {
        public int? ProductDiscount { get; set; }

        public int? CustomerDiscount { get; set; }

        public string Eik { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string CustomerTypeOfRegistration { get; set; }
    }
}
