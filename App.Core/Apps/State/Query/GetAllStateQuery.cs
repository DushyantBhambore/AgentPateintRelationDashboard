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
    public class GetAllStateQuery : IRequest<List<StateDto>>
    {

    }
    public class GetAllStateQueryHandller : IRequestHandler<GetAllStateQuery, List<StateDto>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllStateQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<StateDto>> Handle(GetAllStateQuery request, CancellationToken cancellationToken)
        {
            var list = await _appDbContext.Set<Domain.State>().AsTracking().ToListAsync();
            return list.Adapt<List<StateDto>>();
        }
    }
}
