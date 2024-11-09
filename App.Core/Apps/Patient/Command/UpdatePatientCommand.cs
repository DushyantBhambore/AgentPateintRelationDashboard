using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Apps.Patient.Command
{
    public class UpdatePatientCommand : IRequest<Domain.Patient>
    {
        public PatientDto patient { get; set; }
    }
    public class UpdatePatientCommandHandller : IRequestHandler<UpdatePatientCommand, Domain.Patient>
    {
        private readonly IAppDbContext _appDbContext;
        public UpdatePatientCommandHandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Domain.Patient> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var findid = await _appDbContext.Set<Domain.Patient>().FirstOrDefaultAsync(x => x.Id == request.patient.Id && x.IsActive == true);

            if( findid != null)
            {
                findid.FirstName = request.patient.FirstName;
                findid.LastName = request.patient.LastName;
                findid.Email = request.patient.Email;
                findid.DateOfBirth = request.patient.DateOfBirth;
                findid.Age = request.patient.Age;
                findid.Gender = request.patient.Gender;
                findid.BloodGroup = request.patient.BloodGroup;
                findid.AddressLine1 = request.patient.AddressLine1;
                findid.AddressLine2 = request.patient.AddressLine2;
                findid.Country= request.patient.Country;
                findid.State = request.patient.State;
                findid.City = request.patient.City;
                findid.ZipCode = request.patient.ZipCode;
                findid.AppoinmentDate = request.patient.AppoinmentDate;
                findid.IsActive = request.patient.IsActive;
                findid.IsDelete = request.patient.IsDelete;
                await _appDbContext.SaveChangesAsync(cancellationToken);
                return findid.Adapt<Domain.Patient>();
            }
            return null;
        }
    }
}
