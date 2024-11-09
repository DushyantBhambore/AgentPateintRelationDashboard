using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Country.Command
{
    public class AddCountryCommand : IRequest<CountryDto>
    {
        public CountryDto country { get; set; }
    }
    public class AddCountryCommandHandller : IRequestHandler<AddCountryCommand, CountryDto>
    {
        private readonly IAppDbContext _appDbContext;

        public AddCountryCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<CountryDto> Handle(AddCountryCommand request, CancellationToken cancellationToken)
        {
            var addcountry = new Domain.Country
            {
                CountryId = request.country.CountryId,
                CountryName = request.country.CountryName
            };
            await _appDbContext.Set<Domain.Country>().AddAsync(addcountry);
           await _appDbContext.SaveChangesAsync();
            return addcountry.Adapt<CountryDto>();
        }
    }
}
