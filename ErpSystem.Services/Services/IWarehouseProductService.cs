using System.Collections.Generic;
using ErpSystem.Models;

namespace ErpSystem.Services.Services
{
    public interface IWarehouseProductService
    {
        WarehouseProduct GetById(int Id);

        IEnumerable<KeyValuePair<string, int>> CurrentSpace();
    }
}
