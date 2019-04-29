using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sawa2ly.Models
{
    public class Posts
    {
        public int Id { get; set; }

        [Required]
        public String Message { get; set; }


        [Column(TypeName = "Date")]
        public DateTime? Date { get; set; }

        public Project Project { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        public ApplicationUser User { get; set; }

        public String UserID { get; set; }

    }
}