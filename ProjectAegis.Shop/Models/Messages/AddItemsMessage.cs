namespace ProjectAegis.Shop.Models.Messages
{
    using System.Collections.Generic;

    using Models;

    public class AddItemsMessage
    {
        public IEnumerable<Item> Items { get; set; }
    }
}