namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;

    using ErpSystem.Models;

    public interface IWarehouseProductService
    {
        WarehouseProduct GetById(int id);

        IEnumerable<KeyValuePair<string, int>> CurrentSpace();
    }
}
