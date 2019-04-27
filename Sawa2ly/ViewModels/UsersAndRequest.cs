using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sawa2ly.Models;

namespace Sawa2ly.ViewModels
{
    public class UsersAndRequest
    {
        public List<ApplicationUser> Users { get; set; }
        public List<ProjectsJoinRequests> Requests { get; set; }
    }
}