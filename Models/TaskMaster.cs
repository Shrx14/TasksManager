using System;
using System.ComponentModel.DataAnnotations;

namespace TasksManager.Models
{
    public class TaskMaster
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public string? EmployeeDomainId { get; set; }

        [Required]
        public string? TaskDescription { get; set; }

        public string? SiteCode { get; set; } 

        public string? PlantCode { get; set; } 

        public string? Functions { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Add other relevant columns as needed
    }
}
