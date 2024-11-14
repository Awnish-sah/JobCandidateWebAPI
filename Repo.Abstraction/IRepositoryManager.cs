using Repo.Abstraction.AbstractionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Abstraction
{
    public interface IRepositoryManager
    {
        Task SaveAsync();
        ICandidateRepo Candidate { get; }

    }
}
