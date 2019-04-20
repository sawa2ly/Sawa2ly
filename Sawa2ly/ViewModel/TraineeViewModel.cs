using Sawa2ly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sawa2ly.ViewModel
{
    public class TraineeViewModel
    {
        public List<Project> Project { get; set; }
        public List<ProjectTrainees> ProjectTrainees { get; set; }

    }
}