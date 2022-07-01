

namespace Electronic_Shop.Common
{

    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int Take { get { return PageSize; } }
        public int Skip
        {
            get
            {
                return (Take * (PageNumber - 1));
            }
        }
    }
}
