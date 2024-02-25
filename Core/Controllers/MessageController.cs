using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Controllers
{
    public class MessageController : Controller
    {
        private readonly MessageManager _messageManager = new(new EfMessageRepository());
        private readonly WriterManager _writerManager = new(new EfWriterRepository());
        private readonly UserManager<User> _userManager;

        public MessageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Inbox()
        {
            var values = _messageManager.GetReceivedMessagesByWriter(GetWriterID().Result);
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
        }

        public IActionResult Sendbox()
        {
            var values = _messageManager.GetSentMessagesByWriter(GetWriterID().Result);
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
        }

        public IActionResult DeleteMessage(int id)
        {
            Message message = _messageManager.GetEntityById(id);
            _messageManager.DeleteEntity(message);

            return RedirectToAction("Inbox", "Message");
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            PopulateWritersDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            MessageValidator messageValidator = new();
            ValidationResult result = messageValidator.Validate(message);

            if (result.IsValid)
            {
                message.SenderID = GetWriterID().Result;
                message.ReceiverID = message.ReceiverID;
                message.MessageDate = System.DateTime.Now;
                message.MessageStatus = true;
                _messageManager.AddEntity(message);

                return RedirectToAction("Inbox", "Message");
            }
            else
            {
                PopulateWritersDropdown();

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }

            return View();
        }

        private async Task<int> GetWriterID()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            string userId = await _userManager.GetUserIdAsync(user);
            var writer = _writerManager.GetWriterBySession(userId);

            return writer.WriterID;
        }

        private void PopulateWritersDropdown()
        {
            List<SelectListItem> receivers = (from x in _writerManager.GetEntities()
                                              where x.WriterStatus
                                              select new SelectListItem
                                              {
                                                  Text = x.WriterNameSurname,
                                                  Value = x.WriterID.ToString()
                                              }).ToList();
            ViewBag.receivers = receivers;
        }
    }
}
