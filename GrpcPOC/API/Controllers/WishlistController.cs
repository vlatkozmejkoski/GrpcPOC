using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}