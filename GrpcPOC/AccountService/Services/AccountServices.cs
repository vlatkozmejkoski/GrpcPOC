using Grpc.Core;
using Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountService.Services
{
    public class AccountServices : Account.AccountBase
    {
        public List<Tuple<string, Guid>> Users { get; set; } = new List<Tuple<string, Guid>>();
        private readonly HttpClient _client;

        public AccountServices(HttpClient client)
        {
            _client = client;
            Users.Add(new Tuple<string, Guid>("Vlatko", Guid.NewGuid()));
        }

        public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
        {
            var c = await _client.GetAsync("/test").ConfigureAwait(false);
            var userId = Guid.NewGuid();
            Users.Add(new Tuple<string, Guid>(request.Name, userId));

            var response = new AddUserResponse()
            {
                Message = $"Successfully added user {request.Name}, with email {request.Email}!",
                UserId = userId.ToString()
            };
            return await Task.FromResult(response).ConfigureAwait(false);
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
