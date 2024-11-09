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
    public class GetCityByIdQuery :IRequest<List<CityDto>>
    {
        public int Id { get; set; }
    }
    public class GetCityByIdQueryHandller : IRequestHandler<GetCityByIdQuery, List<CityDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetCityByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CityDto>> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _appDbContext.Set<Domain.City>().Where(x => x.StateId == request.Id).ToListAsync();
            return list.Adapt<List<CityDto>>();
        }
    }
}
