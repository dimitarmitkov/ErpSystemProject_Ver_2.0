using System;
using System.Collections.Generic;

namespace ErpSystem.Services.Services
{
    public interface IWarehouseSpace
    {
        IEnumerable<KeyValuePair<string, int>> GetSpaceBoxes();

        IEnumerable<KeyValuePair<string, int>> GetSpacePallets();
    }
}
