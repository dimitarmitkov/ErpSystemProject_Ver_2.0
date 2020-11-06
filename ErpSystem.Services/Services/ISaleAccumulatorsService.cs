using System;
namespace ErpSystem.Services.Services
{
    public interface ISaleAccumulatorsService
    {
        void CountBoxes();

        void RemoveBoxFromWarehouse();

        void CountPallets();

        void RemovePalletFromWarehouse();
    }
}
