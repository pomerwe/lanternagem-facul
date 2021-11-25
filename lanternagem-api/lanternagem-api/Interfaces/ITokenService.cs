using lanternagem_api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(SystemUser systemUser);
    }
}
