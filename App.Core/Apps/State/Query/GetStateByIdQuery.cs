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

namespace App.Core.Apps.State.Query
{
    public class GetStateByIdQuery : IRequest<List<StateDto>>
    {
        public int Id { get; set; }
    }
    public class GetStateByIdQueryHandller : IRequestHandler<GetStateByIdQuery, List<StateDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetStateByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<StateDto>> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
        {
            var findid = await _appDbContext.Set<Domain.State>().Where(x=>x.CountryId == request.Id).ToListAsync();

            if (findid == null)
            {
                return null;
            }
            return findid.Adapt<List<StateDto>>();

        }
    }
}
