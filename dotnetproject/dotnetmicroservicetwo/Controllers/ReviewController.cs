using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnetmicroservicetwo.Models;

namespace dotnetmicroservicetwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ReviewDbContext _context;

        public ReviewController(ReviewDbContext context)
        {
            _context = context;
        }

    }
}
