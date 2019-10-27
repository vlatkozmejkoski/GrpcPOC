using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Protos.Account;

namespace WishlistService.Services
{
    public class WishlistServices : Wishlist.WishlistBase
    {
        private readonly AccountClient _accountClient;
        private List<GetWishesResponse> wishes = new List<GetWishesResponse>();

        public WishlistServices(AccountClient accountClient)
        {
            _accountClient = accountClient;
            var list = new List<GetWishesResponse>() {
                new GetWishesResponse
                {
                    Category = "Books",
                    Name = "Harry Potter and the Deathly Hallows Part 1",
                    Quantity = 1
                },
                new GetWishesResponse
                {
                    Category = "Books",
                    Name = "Harry Potter and the Deathly Hallows Part 2",
                    Quantity = 1
                }
            };
            wishes.AddRange(list);
        }

        public override Task<AddWishResponse> AddWish(AddWishRequest request, ServerCallContext context)
        {
            var user = _accountClient.GetUser(new GetUserRequest() { Name = "Vlatko" });
            return Task.FromResult(new AddWishResponse()
            {
                Message = $"Hello {user.Name}!The product {request.Product.Name} was added to your wishlist! Quantity: {request.Quantity}"
            });

        }

        public override async Task GetWishes(Empty request, IServerStreamWriter<GetWishesResponse> responseStream, ServerCallContext context)
        {
            foreach(var wish in wishes)
            {
                await responseStream.WriteAsync(wish);
            }
        }
    }
}
