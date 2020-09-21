namespace LuyenThiOnline.DTOs
{
    public class QuestionForExamDTO
    {
        public int Id { get; set; }
        public string QuestionContent { get; set; }      
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public int QuestionPoint { get; set; }
    }
}