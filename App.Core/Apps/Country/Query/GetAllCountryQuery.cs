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

namespace App.Core.Apps.Country.Query
{
    public class GetAllCountryQuery : IRequest<List<CountryDto>>
    {
    }
    public class GetAllCountryQueryHandller : IRequestHandler<GetAllCountryQuery, List<CountryDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllCountryQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<CountryDto>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {

            var list = await _appDbContext.Set<Domain.Country>().AsTracking().ToListAsync();
            return list.Adapt<List<CountryDto>>();  
        }
    }
}
