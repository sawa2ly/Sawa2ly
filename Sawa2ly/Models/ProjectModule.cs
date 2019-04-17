using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sawa2ly.Models
{
    public class ProjectModule
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        public int Status { get; set; } // 1 -> Delivered   0 -> OnProgress

        public Double Price { get; set; }

    }
}