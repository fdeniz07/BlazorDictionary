namespace BlazorDictionary.Common.Models.Page
{
    public class BasePagedQuery
    {
        public BasePagedQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        public int Page{ get; set; }

        public int PageSize { get; set; }
    }
}
