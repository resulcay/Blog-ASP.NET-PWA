using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        readonly Message2Manager messageManager = new(new EfMessage2Repository());
        readonly WriterManager writerManager = new(new EfWriterRepository());
        readonly Context context = new();

        public IActionResult Inbox()
        {
            var values = messageManager.GetDetailedMessages();
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
        }

        public IActionResult Sendbox()
        {
            var values = messageManager.GetSentMessagesByWriter(GetWriterID());
            values = values.OrderByDescending(x => x.MessageDate).ToList();
            return View(values);
        }

        public IActionResult DeleteMessage(int id)
        {
            Message2 message2 = messageManager.GetEntityById(id);
            messageManager.DeleteEntity(message2);

            return RedirectToAction("Inbox", "Message");
        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            PopulateWritersDropdown();
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message2 message)
        {
            string summernoteHtml = message.MessageDetails;
            string nonHtmlText = ExtractNonHtmlText(summernoteHtml);
            message.MessageDetails = nonHtmlText;

            Message2Validator messageValidator = new();
            ValidationResult result = messageValidator.Validate(message);

            if (result.IsValid)
            {
                message.SenderID = GetWriterID();
                message.ReceiverID = message.ReceiverID;
                message.MessageDate = System.DateTime.Now;
                message.MessageStatus = true;
                messageManager.AddEntity(message);

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

        private int GetWriterID()
        {
            var userName = User.Identity.Name;
            var userMail = context.Users.Where(x => x.UserName == userName).Select(x => x.Email).FirstOrDefault();
            var writerID = writerManager.GetWriterIDBySession(userMail);

            return writerID;
        }

        private void PopulateWritersDropdown()
        {
            List<SelectListItem> receivers = (from x in writerManager.GetEntities()
                                              select new SelectListItem
                                              {
                                                  Text = x.WriterName,
                                                  Value = x.WriterID.ToString()
                                              }).ToList();
            ViewBag.receivers = receivers;
        }
    }
}
