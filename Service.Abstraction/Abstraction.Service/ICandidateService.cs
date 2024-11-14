using Contract.DTO;
using Contract.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction.Abstraction.Service
{
    public interface ICandidateService
    {
        Task<DataResult<CandidateDTO>> AddOrUpdateCandidate(CandidateDTO candidateDTO);


    }
}
