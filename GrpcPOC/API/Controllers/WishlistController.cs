using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Protos;
using static Protos.Wishlist;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly WishlistClient _wishlistClient;

        public WishlistController(WishlistClient wishlistClient)
        {
            _wishlistClient = wishlistClient;
        }

        [HttpPost]
        public AddWishResponse AddWish(AddWishRequest request)
        {
            return _wishlistClient.AddWish(request);
        }

        [HttpGet]
        public IAsyncEnumerable<GetWishesResponse> GetWishes()
        {
            var wishes = _wishlistClient.GetWishes(new Google.Protobuf.WellKnownTypes.Empty());
            return wishes.ResponseStream.ReadAllAsync();
        }
    }
}