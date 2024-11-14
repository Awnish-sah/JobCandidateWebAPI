using Common.Area.Base.Controllers;
using Contract.DTO;
using Contract.Enum;
using Extensions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Area.Controllers
{
    /// <summary>
    /// This api handles all the logic for Candidate.
    /// </summary>
    [Area("Global")]
    [ApiController]
    [Route("api/Global/[controller]")]
    public class CandidateController : BaseController
    {
        public CandidateController(IServiceManager service) : base(service)
        {
        }

        /// <summary>
        /// Post Candidate.
        /// </summary>
        /// <param name="candidateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<DataResult<CandidateDTO>> AddOrUpdateCandidate(CandidateDTO candidateDTO)
        {
            DataResult<CandidateDTO> dataResult;

            if (candidateDTO == null)
            {
                throw new BadRequestException("Invalid Input, Null Data Received");
            }

            if (!ModelState.IsValid)
            {
                throw new BadRequestException(string.Join(";", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
            }
            try
            {
                dataResult = await _service.CandidateService.AddOrUpdateCandidate(candidateDTO);
                dataResult = new DataResult<CandidateDTO> { Success = true, Message = dataResult.Message, Status = dataResult.Status, Data = dataResult.Data };
                return dataResult;

            }
            catch
            {
                throw;
            }

        }
    }
}
