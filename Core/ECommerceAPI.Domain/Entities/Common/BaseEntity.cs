using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Domain.Entities.Common
{
    public class BaseEntity
    {

        public Guid Id { get; set; } // Guid = Uniqe Identity Field
        public DateTime CreatedDate { get; set; }


    }
}
