using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LuyenThiOnline.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int EnrolledCount { get; set; }
        public int PassScore { get; set; }
        public int ExpGain { get; set; }
        public int RankId { get; set; }
        [ForeignKey("RankId")]
        public virtual Rank Rank { get; set; }
        public virtual IEnumerable<Question> Questions { get; set; }
        public virtual IEnumerable<History> Histories { get; set; }
    }
}
