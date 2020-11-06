using System;
using ErpSystem.Services.ViewModels.Order;

namespace ErpSystem.Services.Services
{
    public interface IOrdersService
    {
        void GenetareOrder(GenerateOrderViewModel generateOrder);
    }
}
