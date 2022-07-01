using System.Collections.Generic;

namespace Electronic_Shop.Common
{
    public class ListModel<T>
    {
        public ListModel()
        {
            Items = new List<T>();
        }
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
