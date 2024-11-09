using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Apps.Patient.Query
{
    public class GetPatientByIdQuery : IRequest<List<Domain.Patient>>
    {
        public int id { get; set; }
    }
    public class GetPatientByIdQueryHandller : IRequestHandler<GetPatientByIdQuery, List<Domain.Patient>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetPatientByIdQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Domain.Patient>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var findid = await  _appDbContext.Set<Domain.Patient>().Where(x=>x.UserId == request.id).ToListAsync();
            if(findid == null)
            {
                return null;
            }
            return findid.Adapt<List<Domain.Patient>>();
        }
    }

}
