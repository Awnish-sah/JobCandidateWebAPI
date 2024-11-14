using Domain;
using Repo.Abstraction.AbstractionRepo;
using Repo.Abstraction;
using Repository.Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly ApplicationContext _context;
    private readonly ICandidateRepo _candidateRepo;

    public RepositoryManager(ApplicationContext context)
    {
        _context = context;
    }

    public ICandidateRepo Candidate
    {
        get
        {
            return _candidateRepo ?? new CandidateRepo(_context);
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
