﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sawa2ly.Models
{
    public class ProjectTrainees
    {
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        public ApplicationUser MTS { get; set; }

        public String MTSID { get; set; }

    }
}