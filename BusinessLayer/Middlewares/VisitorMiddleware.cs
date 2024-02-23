using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BusinessLayer.Middlewares
{
    public class VisitorMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly VisitorManager _visitorManager = new(new EfVisitorRepository());

        public VisitorMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                bool isIpUnique = _visitorManager.IsVisitorUnique(ipAddress);

                if (isIpUnique)
                {
                    Visitor visitor = new()
                    {
                        VisitorIp = ipAddress,
                        TimeStamp = System.DateTime.Now
                    };

                    _visitorManager.AddEntity(visitor);
                }
            }

            await _requestDelegate(context);
        }
    }
}
