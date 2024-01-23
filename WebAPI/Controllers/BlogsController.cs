using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        readonly BlogManager manager = new(new EfBlogRepository());

        [HttpGet]
        public IActionResult GetBlogList()
        {
            var result = manager.GetEntities();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogById(int id)
        {
            var result = manager.GetEntityById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
