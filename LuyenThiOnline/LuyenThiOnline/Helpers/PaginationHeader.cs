namespace LuyenThiOnline.Helpers
{
    public class PaginationHeader
    {
        public int CurrenPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public PaginationHeader(int currentPage, int itemPerPage, int totalItems, int totalPages)
        {
            CurrenPage = currentPage;
            ItemsPerPage = itemPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
    }
}