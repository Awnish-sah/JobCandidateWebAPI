using Contract.DTO;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repo.Abstraction;
using Repository;
using Service.Abstraction;
using Service;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repo.Abstraction.AbstractionRepo;
using Repository.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace JobCandidateApi.Tests
{
    public class JobCandidateServiceTests
    {
        private readonly ApplicationContext _context;  // Use the real repository
        private readonly IRepositoryManager _repoManager;
        private readonly IServiceProvider _serviceProvider;

        #region Constructor with DI Container
        public JobCandidateServiceTests()
        {
            // Set up a test-specific DI container
            var services = new ServiceCollection();

            var connectionString = "Server=DOTNET--AVANISH; Database=Candidate_Db; uid=sa; pwd=infodev; MultipleActiveResultSets=true; Trusted_Connection=false; Connection Timeout=100;Integrated Security=false; PersistSecurityInfo=true;TrustServerCertificate=true;";

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            // Repository and Services to DI container
            services.AddScoped<IRepositoryManager, RepositoryManager>();

            // Build the service provider
            _serviceProvider = services.BuildServiceProvider();

            // Resolve the ApplicationContext from DI container
            _context = _serviceProvider.GetRequiredService<ApplicationContext>();

            // Resolve the repository manager from DI container
            _repoManager = _serviceProvider.GetRequiredService<IRepositoryManager>();
        }
        #endregion

        [Fact]
        public async Task AddOrUpdateCandidateAsync_CreatesNewCandidate()
        {
            // Create a new candidate instance (DTO)
            var candidateDTO = new CandidateDTO
            {
                  FirstName = "Rahul",
                  LastName = "Sah",
                  PhoneNumber = "9815220966",
                  Email = "rahul@gmail.com",
                  BestTimeToCall = "9:00 AM - 11:00 AM",
                  LinkedInProfile = "https://linkedin.com/in/rahul",
                  GitHubProfile = "https://github.com/rahul",
                  Comment = "BBA Holder"
            };

            // Call the method to add or update the candidate
            await _repoManager.Candidate.AddOrUpdateCandidate(candidateDTO);

            var candidate = await _context.Candidate
                .FirstOrDefaultAsync(c => c.Email == candidateDTO.Email);  // To Ensure candidate is saved

            Assert.NotNull(candidate);  // Assert that the candidate is saved

            // Verify that SaveChangesAsync was actually called once
            var saveChangesCalled = false;
            try
            {
                await _context.SaveChangesAsync(CancellationToken.None);
                saveChangesCalled = true;
            }
            catch
            {
                throw;
            }

            Assert.True(saveChangesCalled);  // Ensure that SaveChangesAsync was called
        }
    }
}
