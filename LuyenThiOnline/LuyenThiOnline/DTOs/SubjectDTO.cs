using System.Collections.Generic;
using LuyenThiOnline.Models;

namespace LuyenThiOnline.DTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int EnrolledCount { get; set; }
        public int PassScore { get; set; }
        public int ExpGain { get; set; }
        public string RankName { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
    }
}