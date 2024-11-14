using Contract.DTO;
using Contract.Enum;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repo.Abstraction;
using Service.Abstraction.Abstraction.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class CandidateService : ICandidateService
    {
        private readonly IRepositoryManager _repoManager;
        public CandidateService(IRepositoryManager repoManager)
        {
            _repoManager = repoManager;
        }


        public async Task<DataResult<CandidateDTO>> AddOrUpdateCandidate(CandidateDTO candidateDTO)
        {
            var existingCandidate = await _repoManager.Candidate.GetAll()
                                                                .Where(x => x.Email == candidateDTO.Email)
                                                                .FirstOrDefaultAsync();

            try
            {
                if (existingCandidate != null)
                {
                    // Update existing record
                    existingCandidate.FirstName = candidateDTO.FirstName;
                    existingCandidate.LastName = candidateDTO.LastName;
                    existingCandidate.PhoneNumber = candidateDTO.PhoneNumber;
                    existingCandidate.BestTimeToCall = candidateDTO.BestTimeToCall;
                    existingCandidate.LinkedInProfile = candidateDTO.LinkedInProfile;
                    existingCandidate.GitHubProfile = candidateDTO.GitHubProfile;
                    existingCandidate.Comment = candidateDTO.Comment;
                    existingCandidate.LastUpdated = DateTime.UtcNow;

                    _repoManager.Candidate.Update(existingCandidate);
                }
                else
                {
                    // Create new record
                    Candidate candidate = new Candidate
                    {
                        FirstName = candidateDTO.FirstName,
                        LastName = candidateDTO.LastName,
                        PhoneNumber = candidateDTO.PhoneNumber,
                        Email = candidateDTO.Email,
                        BestTimeToCall = candidateDTO.BestTimeToCall,
                        Comment = candidateDTO.Comment,
                        LastUpdated = DateTime.UtcNow,
                        GitHubProfile = candidateDTO.GitHubProfile,
                        LinkedInProfile = candidateDTO.LinkedInProfile

                    };

                    _repoManager.Candidate.Insert(candidate);
                }

                await _repoManager.SaveAsync();

                var result = new DataResult<CandidateDTO>
                {
                    Success = true,
                    Message = existingCandidate?.Email == candidateDTO.Email ? "Successfully updated." : "Successfully inserted.",
                    Data = new CandidateDTO
                    {
                        Id = candidateDTO.Id,
                        FirstName = candidateDTO.FirstName,
                        LastName = candidateDTO.LastName,
                        PhoneNumber = candidateDTO.PhoneNumber,
                        Email = candidateDTO.Email,
                        BestTimeToCall = candidateDTO.BestTimeToCall,
                        Comment = candidateDTO.Comment,
                        GitHubProfile = candidateDTO.GitHubProfile,
                        LinkedInProfile = candidateDTO.LinkedInProfile,
                        
                    },
                    Status = StatusCodes.Status200OK
                };

                return result;
            }
            catch (Exception ex)
            {
                return new DataResult<CandidateDTO>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Status = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}

