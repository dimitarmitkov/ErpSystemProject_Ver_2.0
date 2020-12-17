namespace ErpSystem.Services.Services
{
    using System.Collections.Generic;

    public interface IWarehouseSpace
    {
        IEnumerable<KeyValuePair<string, int>> GetSpaceBoxes();

        IEnumerable<KeyValuePair<string, int>> GetSpacePallets();
    }
}
