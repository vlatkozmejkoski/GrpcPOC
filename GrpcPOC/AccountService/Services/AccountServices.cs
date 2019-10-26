using Grpc.Core;
using Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Services
{
    public class AccountServices : Account.AccountBase
    {
        public List<Tuple<string, Guid>> Users { get; set; } = new List<Tuple<string, Guid>>();

        public AccountServices()
        {
            Users.Add(new Tuple<string, Guid>("Vlatko", Guid.NewGuid()));
        }

        public override Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
        {
            var userId = Guid.NewGuid();
            Users.Add(new Tuple<string, Guid>(request.Name, userId));

            var response = new AddUserResponse()
            {
                Message = $"Successfully added user {request.Name}, with email {request.Email}!",
                UserId = userId.ToString()
            };
            return Task.FromResult(response);
        }

        public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            var user = Users.FirstOrDefault(x => x.Item1.Equals(request.Name));
            return Task.FromResult(new GetUserResponse()
            {
                Name = user.Item1,
                UserId = user.Item2.ToString()
            });
        }
    }
}
