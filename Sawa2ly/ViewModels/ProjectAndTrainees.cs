using Sawa2ly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sawa2ly.ViewModels
{
    public class ProjectAndTrainees
    {
        public Project Project { get; set; }
        public List<ProjectTrainees> ProjectTrainees { get; set; }
    }
}