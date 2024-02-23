
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class VisitorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
      //  private readonly VisitorManager _visitorManager = new VisitorManager(new EfVisitorRepository());

        public VisitorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString();



            await _requestDelegate(context);
        }
    }
}
