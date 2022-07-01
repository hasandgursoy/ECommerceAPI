﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance.Repositories.InvoiceFile
{
    public class InvoiceWriteRepository : WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
