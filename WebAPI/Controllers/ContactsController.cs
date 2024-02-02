using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace WebAPI.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactManager manager = new(new EfContactRepository());

        [HttpGet("all")]
        public IActionResult GetContactList(string? format)
        {
            var result = manager.GetAll();

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
        public IActionResult GetContactById(int contactId, string? format)
        {
            var result = manager.GetEntityById(contactId);

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
        public IActionResult AddContact(Contact contact, string? format)
        {
            try
            {
                manager.ContactAdd(contact);
                var result = manager.GetEntityById(contact.ContactID);

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