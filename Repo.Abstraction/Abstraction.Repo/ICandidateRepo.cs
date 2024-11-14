using Contract.DTO;
using Contract.Enum;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Abstraction.AbstractionRepo
{
    public interface ICandidateRepo : IGenericRepo<Candidate>
    {
        Task AddOrUpdateCandidate(CandidateDTO candidateDTO);

    }
}
