using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GtMotive.Microservice.Domain.DomainContracts;

public interface IDomainRule
{
    bool IsSatisfied();
    string ErrorMessage { get; }
}
