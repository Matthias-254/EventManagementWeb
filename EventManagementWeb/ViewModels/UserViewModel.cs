﻿using EventManagementWeb.Data;
using EventManagementWeb.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EventManagementWeb.ViewModels
{
    public class UserViewModel
    {
        [Key]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } //Combined first and last name

        [Display(Name = "Roles")]
        public List<string> Roles { get; set; }

        [Display(Name = "Blocked?")]
        public bool IsBlocked { get; set; } = false;

        public UserViewModel()
        {
        }

        public UserViewModel(EventManagementUser user, ApplicationDbContext context)
        {
            UserId = user.Id;
            UserName = user.UserName;
            UserEmail = user.Email;
            Name = user.ToString();
            IsBlocked = user.LockoutEnd > DateTime.Now;
            Roles = (from userRole in context.UserRoles
                    where userRole.UserId == user.Id
                    select userRole.RoleId).ToList();
        }
    }
}
