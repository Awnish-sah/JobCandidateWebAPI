﻿using Service.Abstraction.Abstraction.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstraction
{
    public interface IServiceManager
    {
        ICandidateService CandidateService { get; }
    }
}
