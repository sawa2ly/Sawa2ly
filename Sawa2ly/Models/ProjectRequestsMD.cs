using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sawa2ly.Models
{
    public class ProjectRequestsMD
    {
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        public ApplicationUser MD { get; set; }

        public String MDID { get; set; }

        public ApplicationUser Customer { get; set; }

        public String CustomerId { get; set; }

    }
}