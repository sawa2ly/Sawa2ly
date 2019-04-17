using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sawa2ly.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        public String ProjectImageUrl { get; set; }

        public ApplicationUser Customer { get; set; }

        [Required]
        public String CustomerId { get; set; }

        public ApplicationUser MD { get; set; }

        public String MDID { get; set; }

        public ProjectModule ProjectModule { get; set; }

        public int? ProjectModuleId { get; set; }

    }
}