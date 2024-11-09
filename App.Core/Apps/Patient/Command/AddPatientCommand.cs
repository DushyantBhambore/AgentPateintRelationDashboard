using App.Core.Dto;
using App.Core.Interface;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Core.Apps.Patient.Command
{
    public class AddPatientCommand : IRequest<PatientDto>
    {
        public PatientDto patient { get; set; }
    }
    public class AddPatientCommandhandller : IRequestHandler<AddPatientCommand, PatientDto>
    {
        private readonly IAppDbContext _appDbContext;
        public AddPatientCommandhandller(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<PatientDto> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            //var findagentid = await _appDbContext.Set<Domain.User>().Include(a => a.Patients).FirstOrDefaultAsync(a => a.AgentId == request.patient.AgentId);


            //try
            //{
                var findagentid = await _appDbContext.Set<Domain.User>().Include(a => a.Patients).FirstOrDefaultAsync(x => x.AgentId == request.patient.AgentId, cancellationToken);
                if (findagentid == null)
                {
                    return null;
                }

                //var lastpatientid = await _appDbContext.Set<Domain.Patient>().OrderByDescending(x => x.PatinetId).FirstOrDefaultAsync();
                //string newpatientid;
                // Check for the last patient ID associated with this agent.
                var lastpatientid = await _appDbContext.Set<Domain.Patient>().Where(x => x.AgentId == request.patient.AgentId).OrderByDescending(x => x.PatinetId).FirstOrDefaultAsync(cancellationToken);
                string newpatientid;

                // If no patients exist, start from 1.
                if (lastpatientid == null)
                {
                    newpatientid = $"{request.patient.AgentId}00001";
                }
                else
                {
                    var value = int.Parse(lastpatientid.PatinetId.Substring(request.patient.AgentId.Length));
                    newpatientid = $"{lastpatientid.AgentId}{(value + 1).ToString("D5")}";
                }
                var addpatient = new Domain.Patient
                {
                    Id = request.patient.Id,
                    PatinetId = newpatientid,
                    AgentId = request.patient.AgentId,
                    UserId = request.patient.UserId,
                    FirstName = request.patient.FirstName,
                    LastName = request.patient.LastName,
                    Email = request.patient.Email,
                    Gender = request.patient.Gender,
                    BloodGroup = request.patient.BloodGroup,
                    AddressLine1 = request.patient.AddressLine1,
                    AddressLine2 = request.patient.AddressLine2,
                    Country = request.patient.Country,
                    State = request.patient.State,
                    City = request.patient.City,
                    Age = request.patient.Age,
                    ZipCode = request.patient.ZipCode,
                    DateOfBirth = request.patient.DateOfBirth,
                    AppoinmentDate = request.patient.AppoinmentDate,
                    IsActive = request.patient.IsActive,
                    IsDelete = request.patient.IsDelete
                };
                await _appDbContext.Set<Domain.Patient>().AddAsync(addpatient);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                //return  addpatient.Adapt<Domain.Patient>().ToString();
                //return JsonSerializer.Serialize(new
                //{
                //    message = "Patient Added Successfully"
                //});
                //return "Patient Added Successfully";

                return addpatient.Adapt<PatientDto>();


            //}
            //catch (Exception ex)
            //{
            //    // Log the exception details for debugging
            //    Console.WriteLine("Error in AddPatientCommandHandler: " + ex.Message);
            //    throw; // This will propagate the error back to the caller.
            //}
            


        }

    }
}