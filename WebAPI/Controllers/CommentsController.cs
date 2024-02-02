using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentManager manager = new(new EfCommentRepository());

        [HttpGet("all")]
        public IActionResult GetCommentList(string? format)
        {
            var result = manager.GetCommentsWithBlogAndWriter();

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
        public IActionResult GetCommentById(int commentId, string? format)
        {
            var result = manager.GetEntityById(commentId);

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
        public IActionResult UpdateComment(Comment comment, string? format)
        {
            try
            {
                manager.UpdateEntity(comment);
                var result = manager.GetEntityById(comment.CommentID);

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
        public IActionResult AddComment(Comment comment, string? format)
        {
            try
            {
                manager.AddEntity(comment);
                var result = manager.GetEntityById(comment.CommentID);

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
        public IActionResult DeleteComment(int commentId, string? format)
        {
            try
            {
                var comment = manager.GetEntityById(commentId);
                manager.DeleteEntity(comment);

                if (string.Equals(format, "xml", StringComparison.OrdinalIgnoreCase))
                {
                    return Content($"<response>Successfully Deleted</response>", "application/xml");
                }
                else
                {
                    var response = new { response = "Successfully Deleted" };
                    var jsonResponse = JsonConvert.SerializeObject(response);

                    return Content(jsonResponse, "application/json");
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