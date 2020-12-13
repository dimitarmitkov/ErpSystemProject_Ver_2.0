using System.Collections.Generic;
using System.Threading.Tasks;
using ErpSystem.Services.ViewModels.Customer;

namespace ErpSystem.Services.Services
{
    public interface ICustomersService
    {
        Task CreateCustomer(CustomerViewModel customerView);

        Task DeleteCustomer(string id, string companyName);
    }
}
