using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            var values = _messageManager.GetDetailedMessages();
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
            string summernoteHtml = message.MessageDetails;
            string nonHtmlText = ExtractNonHtmlText(summernoteHtml);
            message.MessageDetails = nonHtmlText;

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

        private static string ExtractNonHtmlText(string html)
        {
            html ??= "";

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            string extractedText = ExtractTextFromNodes(doc.DocumentNode.ChildNodes);

            string nonHtmlText = Regex.Replace(extractedText, "<.*?>", string.Empty);

            return nonHtmlText.Trim();
        }

        private static string ExtractTextFromNodes(HtmlNodeCollection nodes)
        {
            string text = "";

            foreach (var node in nodes)
            {
                if (node.NodeType == HtmlNodeType.Text)
                {
                    text += node.InnerText + " ";
                }
                else if (node.NodeType == HtmlNodeType.Element)
                {
                    if (node.Name.ToLower() != "script" && node.Name.ToLower() != "style")
                    {
                        text += ExtractTextFromNodes(node.ChildNodes);
                    }
                }
            }

            return text.Trim();
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
                                              select new SelectListItem
                                              {
                                                  Text = x.WriterNameSurname,
                                                  Value = x.WriterID.ToString()
                                              }).ToList();
            ViewBag.receivers = receivers;
        }
    }
}
