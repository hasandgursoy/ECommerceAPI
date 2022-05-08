using ECommerceAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{   
    // Normalde IRepository'de bütün yapılanmalar bulunurdu. Write and Read bir aradaydı.
    // Biz Write ve Read olarak ayırdık şuanda da base class tanımlıyoruz.
    // Solid prensiplerine zarar vermeden mantığından çıkmadan işi halletmeye çalışıyorum.
    public interface IRepository<T> where T : BaseEntity
    {

        DbSet<T> Table { get;}

    }
}
