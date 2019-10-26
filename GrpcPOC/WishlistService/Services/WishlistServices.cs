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

        public WishlistServices(AccountClient accountClient)
        {
            _accountClient = accountClient;
        }

        public override Task<AddWishResponse> AddWish(AddWishRequest request, ServerCallContext context)
        {
            var user = _accountClient.GetUser(new GetUserRequest() { Name = "Vlatko" });
            return Task.FromResult(new AddWishResponse()
            {
                Message = $"Hello {user.Name}!\nThe product {request.Product.Name} was added to your wishlist! Quantity: {request.Quantity}"
            });

        }
    }
}
