using Contract.DTO;
using Contract.Enum;
using Domain;
using Domain.Entities;
using Repo.Abstraction.AbstractionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CandidateRepo : GenericRepo<Candidate>, ICandidateRepo
    {
        public CandidateRepo(ApplicationContext context) : base(context)
        {
        }

        // Candidate Repo for Unit Testing purposes only.
        public async Task AddOrUpdateCandidate(CandidateDTO candidateDTO)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Check if a candidate with the same email already exists
                    var existingCandidate = _context.Candidate
                                                    .FirstOrDefault(x => x.Email == candidateDTO.Email);

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

                        _context.Candidate.Update(existingCandidate);
                    }
                    else
                    {
                        // Create new record
                        var candidate = new Candidate
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

                        _context.Candidate.Add(candidate);
                    }

                    // Save changes and commit transaction
                    await _context.SaveChangesAsync();
                    await dbContextTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    throw new Exception($"An error occurred while adding or updating the candidate: {ex.Message}", ex);
                }
            }
        }

    }

}
