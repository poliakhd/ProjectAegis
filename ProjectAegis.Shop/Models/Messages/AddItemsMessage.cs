using System.Collections.Generic;

namespace ProjectAegis.Shop.Models.Messages
{
    public class AddItemsMessage
    {
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }

        public IEnumerable<int> Items { get; set; }
    }
}