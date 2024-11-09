using App.Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.Patient.Command
{
    public class DeletePatientCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
    public class DeletePatientCommandHandller : IRequestHandler<DeletePatientCommand , string>
    {
        private readonly IAppDbContext _appDbContext;

        public DeletePatientCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var findid = await _appDbContext.Set<Domain.Patient>().Where(x=>x.Id == request.Id && x.IsActive==true).FirstOrDefaultAsync();
            if (findid == null )
            {
                return JsonSerializer.Serialize(new
                {
                    message = "\"Id Not Found\";"
                });
            }
            findid.IsActive = false;
            findid.IsDelete = true;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return JsonSerializer.Serialize(new
            {
                message = "\"Id Deleted Successfully\";"

            });


        }
    }
}
