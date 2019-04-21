using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ApplicationUser MTL { get; set; }

        public String MTLID { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? EndDate { get; set; }

        public int? Status { get; set; } // 1 -> Done   0 -> OnProgress

        public Double? Price { get; set; }

    }
}