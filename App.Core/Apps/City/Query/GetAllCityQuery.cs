using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.City.Query
{
    public class GetAllCityQuery  : IRequest<List<CityDto>>
    {
    }
    public class GetAllCityQueryHandler : IRequestHandler<GetAllCityQuery, List<CityDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllCityQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CityDto>> Handle(GetAllCityQuery request, CancellationToken cancellationToken)
        {
            var list = await _appDbContext.Set<Domain.City>().AsTracking().ToListAsync();
            return list.Adapt<List<CityDto>>();
        }
    }
}
