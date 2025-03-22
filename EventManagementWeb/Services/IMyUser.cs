using EventManagementWeb.Data;
using EventManagementWeb.Models;

namespace EventManagementWeb.Services
{
    public interface IMyUser
    {
        public EventManagementUser User { get; }
    }

    public class MyUser : IMyUser
    {
        ApplicationDbContext _context;
        IHttpContextAccessor _httpcontext;

        public EventManagementUser User { get { return GetUser(); } }

        public MyUser(ApplicationDbContext context, IHttpContextAccessor httpContext) 
        {
            _context = context;
            _httpcontext = httpContext;
        }
        public EventManagementUser GetUser()
        {
            string name = _httpcontext.HttpContext.User.Identity.Name;
            if (name == null || name == "")
            {
                return Globals.DefaultUser;
            }
            else
            {
                return _context.Users.FirstOrDefault(u => u.UserName == name);
            }
        }
    }
}
