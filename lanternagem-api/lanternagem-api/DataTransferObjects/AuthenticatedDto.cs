using lanternagem_api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class AuthenticatedDto
    {
        public SystemUser User { get; set; }
        public string Token { get; set; }
    }
}
