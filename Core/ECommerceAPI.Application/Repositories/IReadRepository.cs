using ECommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>>method,bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T,bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(string id, bool tracking = true);


    }
}

// Tracking
// Tracking mekanizması EF core'da Database'deki değişiklikleri kontrol eden ve bildiren yapdıdır.
// Eğer biz bunu her sorgu için her endpoint için kontrol etsin modunda bırakırsak bu maliyetli bir yapı haline döner.
// O yüzden Read sınıfında amacımız değişiklik yapmak olmadığı için bunu kapatmak daha makul.
// ReadRepository'de bunu yapma şeklimiz var ilk önce Table'ı bir Querayble bir yapıya çeviriyoruz.
// Daha sonra eğer tracking false ise bunu Table.AsNoTracking() şeklinde güncelliyoruz ve istenilen yapıyı dönüyoruz.
