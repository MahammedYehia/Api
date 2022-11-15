using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public string GroupName { get; set; }

        [Required]
        public string Term { get; set; }

        [Required]
        public string Educationallevel { get; set; }
        [Required]
        public DateTime FirstDate { get; set; }
        public DateTime SecondDate { get; set; }
        public DateTime ThirdDate { get; set; }
        public DateTime FourthDate { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}