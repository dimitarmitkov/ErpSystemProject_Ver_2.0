using System.Collections.Generic;
using ErpSystem.Services.ViewModels.Warehouse;

namespace ErpSystem.Services.Services
{
    public interface IWarehousesService
    {
        IEnumerable<KeyValuePair<string, int>> CurrentSpace();
    }
}
