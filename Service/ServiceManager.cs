using Repo.Abstraction;
using Service.Abstraction;
using Service.Abstraction.Abstraction.Service;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly ICandidateService _candidateService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }
        //Candidate Region
        public ICandidateService CandidateService
        {
            get
            {
                return _candidateService ?? new CandidateService(_repositoryManager);
            }
        }
    }
}
