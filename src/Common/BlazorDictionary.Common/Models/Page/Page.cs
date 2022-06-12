namespace BlazorDictionary.Common.Models.Page
{
    public class Page
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public int TotalRowCount { get; set; }

        public int TotalPageCount => (int)Math.Ceiling((double)TotalRowCount / PageSize);

        public int Skip => (CurrentPage - 1) * PageSize;

        public Page()
        {
            
        }

        public Page(int totalRowCount)
        {
            
        }

        public Page(int pageSize, int totalRowCount)
        {
            
        }

        public Page(int currentPage, int pageSize, int totalRowCount)
        {
            if (currentPage < 1)
                throw new ArgumentException("Invalid page number!");
        }
        
    }
}
