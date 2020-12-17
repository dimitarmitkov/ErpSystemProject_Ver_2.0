namespace ErpSystem.Services.Services
{
    using System.Threading.Tasks;
    using ErpSystem.Services.ViewModels.Customer;

    public interface ICustomersService
    {
        Task CreateCustomer(CustomerViewModel customerView);

        Task DeleteCustomer(string id, string companyName);
    }
}
