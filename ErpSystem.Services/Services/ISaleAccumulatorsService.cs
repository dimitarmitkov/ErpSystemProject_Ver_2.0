namespace ErpSystem.Services.Services
{
    using System;

    public interface ISaleAccumulatorsService
    {
        void CountBoxes();

        void RemoveBoxFromWarehouse();

        void CountPallets();

        void RemovePalletFromWarehouse();
    }
}
