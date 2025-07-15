using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasksManager.Models
{
    public class TaskTransaction
    {
        [Key]
        [Column("TaskTransactionId")]
        public int TransactionId { get; set; }

        [ForeignKey("TaskMaster")]
        public int TaskId { get; set; }

        public string? Status { get; set; }

        public DateTime StatusUpdateDate { get; set; } = DateTime.Now;

        public virtual TaskMaster? TaskMaster { get; set; }
    }
}
