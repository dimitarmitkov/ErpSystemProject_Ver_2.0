using System;
using System.Collections.Generic;

namespace ErpSystem.Models
{
    public class CompanyTypeTag
    {
        public CompanyTypeTag()
        {
            this.Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }

        public string CompanyTypeOfRegistration { get; set; } // AD, OOD, EOOD, ET, EAD, ...

        public ICollection<Customer> Customers { get; set; }
    }
}
