﻿namespace ErpSystem.Services.ViewModels.Delivery
{
    using System;
    using System.Collections.Generic;

    public class DeliveryCombinedViewModel
    {
        public IEnumerable<DeliveryListViewModel> List { get; set; }

        public DeliveryListViewModel Single { get; set; }

        public int PageNumber { get; set; }

        public int ProductsCount { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        public int ItemsPerPage { get; set; }
    }
}
