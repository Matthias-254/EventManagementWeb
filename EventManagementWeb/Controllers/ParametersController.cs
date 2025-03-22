using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventManagementWeb.Data;
using EventManagementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using EventManagementWeb.Services;

namespace EventManagementWeb.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class ParametersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EventManagementUser _user;

        public ParametersController(ApplicationDbContext context, IMyUser user)
        {
            _context = context;
            _user = user.User;
        }

        // GET: Parameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parameters.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _EditPartial(string name, string value)
        {
            Parameter parameter = _context.Parameters.First(p => p.Name == name);
            parameter.Value = value;
            parameter.LastChanged = DateTime.Now;
            parameter.UserId = _user.Id;
            _context.Update(parameter);
            Parameter.Parameters[parameter.Name] = parameter;
            if (parameter.Destination == "Mail")
                Parameter.ConfigureMail();
            await _context.SaveChangesAsync();
            return PartialView("_EditPartial", parameter);
        }
    }
}
