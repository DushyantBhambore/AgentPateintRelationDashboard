using App.Core.Dto;
using App.Core.Interface;
using Domain;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Apps.Patient.Query
{
    public class GetAllPatientQuery : IRequest<List<Domain.Patient>>
    {
    }
    public class GetAllPatientQueryHandller : IRequestHandler<GetAllPatientQuery, List<Domain.Patient>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetAllPatientQueryHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Domain.Patient>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            var list = await _appDbContext.Set<Domain.Patient>().Where(a=>a.IsDelete==false && a.IsActive==true).AsTracking().ToListAsync();
            return list.Adapt<List<Domain.Patient>>();
        }
    }
}
