using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.City.Command
{
    public class AddCityCommand : IRequest<CityDto>
    {
        public CityDto city { get; set; }
    }
    public class AddCityCommandHandller : IRequestHandler<AddCityCommand, CityDto>
    {
        private readonly IAppDbContext _appDbContext;

        public AddCityCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CityDto> Handle(AddCityCommand request, CancellationToken cancellationToken)
        {
            var addcity = new Domain.City
            {
                CityId = request.city.CityId,
                StateId = request.city.StateId,
                CityName = request.city.CityName
            };
            await _appDbContext.Set<Domain.City>().AddAsync(addcity);
            await _appDbContext.SaveChangesAsync();
            return addcity.Adapt<CityDto>();
        }
    }
}

