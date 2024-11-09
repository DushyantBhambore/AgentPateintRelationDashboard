using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.State.Command
{
    public class AddStateCommand : IRequest<StateDto>
    {
        public StateDto state { get; set; }
    }
    public class AddStateCommandHandller : IRequestHandler<AddStateCommand, StateDto>
    {
        private readonly IAppDbContext _appDbContext;
        public AddStateCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<StateDto> Handle(AddStateCommand request, CancellationToken cancellationToken)
        {
            var addstate = new Domain.State
            {
                StateId = request.state.StateId,
                CountryId = request.state.CountryId,
                StateName = request.state.StateName
            };
             await _appDbContext.Set<Domain.State>().AddAsync(addstate);
            await _appDbContext.SaveChangesAsync();
            return addstate.Adapt<StateDto>();
        }
    }
}
