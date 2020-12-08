using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ErpSystem.Data;
using ErpSystem.Models;
using ErpSystem.Services.ViewModels.CurrentSale;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ErpSystem.Services.Services
{
    public class CurrentSalesService : ICurrentSalesService
    {
        private readonly ErpSystemDbContext dbContext;

        public CurrentSalesService(ErpSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void GenerateCurrentSale(CurrentSaleViewModel currentSale)
        {
            var customerId = this.dbContext.Customers.Where(c => c.CompanyName == currentSale.CustomerName && c.CompanyEik == currentSale.CustomerEikNumber).Select(x => x.Id).FirstOrDefault();

            var current = new CurrentSale
            {
                CustomerId = customerId,
                CustomerEikNumber = currentSale.CustomerEikNumber,
                CustomerName = currentSale.CustomerName,
                HasCustomerDiscount = currentSale.HasCustomerDiscount,
            };

            this.dbContext.CurrentSales.Add(current);
            this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<SelectListItem> SeclectCustomerDropDown()
        {
            return this.dbContext.Customers.Select(p => new SelectListItem
            {
                Text = p.CompanyName,
                Value = p.CompanyName,

            }).ToList();
        }
    }
}
