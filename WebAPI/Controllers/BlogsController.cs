using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogManager manager = new(new EfBlogRepository());

        [HttpGet("all")]
        public IActionResult GetBlogList([FromQuery] string? format)
        {
            var result = manager.GetDetailedBlogList();

            if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
            {
                var xmlResult = SerializeToXml(result);
                return Content(xmlResult, "application/xml");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpGet("byid/{id}")]
        public IActionResult GetBlogById(int id)
        {
            var result = manager.GetEntityById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        private static string SerializeToXml(object data)
        {
            var serializer = new XmlSerializer(data.GetType());
            using var value = new StringWriter();
            serializer.Serialize(value, data);
            return value.ToString();
        }
    }
}