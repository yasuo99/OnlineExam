using System;

namespace LuyenThiOnline.DTOs
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }  
        public int SubjectId { get; set; }
        public SubjectDTO Subject { get; set; }
        public DateTime DoneDate { get; set; }
        public int Score { get; set; }
    }
}