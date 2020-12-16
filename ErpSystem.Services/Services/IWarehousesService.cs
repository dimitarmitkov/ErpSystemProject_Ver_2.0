namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;

    public interface IWarehousesService
    {
        IEnumerable<KeyValuePair<string, int>> CurrentSpace();
    }
}
