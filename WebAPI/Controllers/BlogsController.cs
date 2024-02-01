using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogManager manager = new(new EfBlogRepository());

        [HttpGet("all")]
        public IActionResult GetBlogList(string? format)
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

        [HttpGet("byid")]
        public IActionResult GetBlogById(int blogId, string? format)
        {
            var result = manager.GetEntityById(blogId);

            if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
            {
                if (result == null)
                {
                    HttpContext.Response.StatusCode = 404;
                    return Content("<response>Not Found</response>", "application/xml");
                }

                var xmlResult = SerializeToXml(result);
                return Content(xmlResult, "application/xml");
            }
            else
            {
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }

        }

        [HttpPut("update")]
        public IActionResult UpdateBlog(Blog blog, string? format)
        {
            try
            {
                manager.UpdateEntity(blog);
                var result = manager.GetEntityById(blog.BlogID);

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
            catch (Exception e)
            {
                if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Response.StatusCode = 404;
                    return Content($"<response><warning>Update Error</warning>devMessage: {e.Message}</response>", "application/xml");
                }

                var errorResponse = new { warning = "Update Error", devMessage = e.Message };
                var jsonError = JsonConvert.SerializeObject(errorResponse);
                HttpContext.Response.StatusCode = 404;

                return Content(jsonError, "application/json");
            }
        }

        [HttpPost("add")]
        public IActionResult AddBlog(Blog blog, string? format)
        {
            try
            {
                manager.AddEntity(blog);
                var result = manager.GetEntityById(blog.BlogID);

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
            catch (Exception e)
            {
                if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Response.StatusCode = 404;
                    return Content($"<response><warning>Addition Error</warning><message>{e.Message}</message></response>", "application/xml");
                }

                var errorResponse = new { warning = "Addition Error", devMessage = e.Message };
                var jsonError = JsonConvert.SerializeObject(errorResponse);
                HttpContext.Response.StatusCode = 404;

                return Content(jsonError, "application/json");
            }
        }

        [HttpDelete("delete")]
        public IActionResult DeleteBlog(int blogId, string? format)
        {
            try
            {
                var blog = manager.GetEntityById(blogId);
                manager.DeleteEntity(blog);

                if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
                {
                    return Content($"<response>Successfully Deleted</response>", "application/xml");
                }
                else
                {
                    var errorResponse = new { response = "Successfully Deleted" };
                    var jsonError = JsonConvert.SerializeObject(errorResponse);

                    return Content(jsonError, "application/json");
                }
            }
            catch (Exception e)
            {
                if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
                {
                    HttpContext.Response.StatusCode = 404;
                    return Content($"<response><warning>Deletion Error</warning><message>{e.Message}</message></response>", "application/xml");
                }

                var errorResponse = new { warning = "Deletion Error", message = e.Message };
                var jsonError = JsonConvert.SerializeObject(errorResponse);
                HttpContext.Response.StatusCode = 404;

                return Content(jsonError, "application/json");
            }
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