using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuyenThiOnline.Models
{
    public class History
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        public DateTime DoneDate { get; set; }
        public int Score { get; set; }
    }
}