using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/newsletters")]
    [ApiController]
    public class NewsLettersController : ControllerBase
    {
        private readonly NewsLetterManager manager = new(new EfNewsLetterRepository());

        [HttpGet("byid")]
        public IActionResult GetNewsLetterById(int newsletterId, string? format)
        {
            var result = manager.GetById(newsletterId);

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

        [HttpPost("add")]
        public IActionResult AddNewsLetter(NewsLetter newsLetter, string? format)
        {
            try
            {
                manager.AddNewsLetter(newsLetter);
                var result = manager.GetById(newsLetter.MailID);

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

        private static string SerializeToXml(object data)
        {
            var serializer = new XmlSerializer(data.GetType());
            using var value = new StringWriter();
            serializer.Serialize(value, data);
            return value.ToString();
        }
    }
}