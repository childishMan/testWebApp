using System;
using Microsoft.EntityFrameworkCore;
using test.DbModels;
using test.Repositories;

namespace test.Services
{
    public class IncidentService:IIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Incident> _incidentRepository;

        public IncidentService(IRepository repository,IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _incidentRepository = repository.GetRepository<Incident>() ??
                                  throw new ArgumentNullException(nameof(repository));
        }

        public void Add(Incident incident)
        {
            _incidentRepository.Add(incident);
            Save();
        }

        private void Save()
        {
            _unitOfWork.SaveChanges();
        }
    }
}