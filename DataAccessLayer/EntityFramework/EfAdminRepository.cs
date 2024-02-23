using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.EntityFramework
{
    public class EfAdminRepository : IAdminDal
    {
        public List<int> SmallQueryData(int id)
        {
            List<int> result = new();
            using var context = new Context();

            result.Add(context.Users.Count());
            result.Add(context.Categories.Count());
            result.Add(context.Messages.Where(z => z.ReceiverID == id).Count());
            result.Add(context.Messages.Where(z => z.SenderID == id).Count());
            result.Add(context.Roles.Count());

            return result;
        }

        public List<int> ComplexQueryData()
        {
            List<int> result = new();
            using var context = new Context();

            result.Add(context.Roles.Count());
            result.Add(context.Notifications.Count());
            result.Add(context.Categories.Where(z => z.CategoryStatus == false).Count());
            result.Add(context.Blogs.Where(z => z.BlogStatus == false).Count());

            return result;
        }

        public List<string> GetRoles()
        {
            using var context = new Context();
            return context.Roles.Select(a => a.Name).ToList();
        }
    }
}
