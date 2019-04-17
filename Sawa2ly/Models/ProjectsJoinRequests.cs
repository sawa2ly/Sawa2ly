using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Sawa2ly.Models
{
    public class ProjectsJoinRequests
    {
        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        public ApplicationUser Sender { get; set; }

        public String SenderId { get; set; }

        public ApplicationUser Reciever { get; set; }

        public String RecieverId { get; set; }
    }
}