using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Protos;
using static Protos.Account;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountClient _accountClient;

        public AccountController(AccountClient accountClient, ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountClient = accountClient;
        }

        [HttpPost]
        public AddUserResponse Post(AddUserRequest request)
        {
            return _accountClient.AddUser(request);
        }
    }
}
