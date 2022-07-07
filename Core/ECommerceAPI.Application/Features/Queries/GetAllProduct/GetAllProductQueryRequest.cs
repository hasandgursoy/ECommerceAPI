using ECommerceAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Features.Queries.GetAllProduct
{

    // Alltaki tanımlamada Request parametresi olarak istenilen şeyi aldık ilk önce.
    // Daha sonra Request'in ve Resposne'un belli olması için MediatR sayesinde IRequest implementasyonu ile bu durumu bildiriyoruz.
    // En sonunda handler sınıfını bildiricez Ama burda değil ilgili yerde.
    public class GetAllProductQueryRequest : IRequest<GetAllProductQueryResponse>
    {
        //public Pagination Pagination { get; set; }

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
