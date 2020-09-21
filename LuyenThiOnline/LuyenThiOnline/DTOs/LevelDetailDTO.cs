using System.Collections.Generic;

namespace LuyenThiOnline.DTOs
{
    public class LevelDetailDTO
    {
        public int Id { get; set; }
        public string LevelName { get; set; }
        public int StartExp { get; set; }
        public int EndExp { get; set; }
        public IEnumerable<AccountForDetailDTO> Accounts { get; set; }
    }
}