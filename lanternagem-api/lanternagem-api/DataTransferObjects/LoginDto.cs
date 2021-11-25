using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lanternagem_api.DataTransferObjects
{
    public class LoginDto 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
