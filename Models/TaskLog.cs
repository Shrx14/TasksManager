using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManager.Models
{
    [Table("TaskLogs")]
    public class TaskLog
    {
        [Key]
        [Column(Order = 0)]
        public int TaskId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime StatusUpdateDate { get; set; }

        [Column("Status")]
        public string? Status { get; set; }

        [Column("PreviousStatus")]
        public string? PreviousStatus { get; set; }

        [Column("Remarks")]
        public string? Remarks { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }
    }
}
