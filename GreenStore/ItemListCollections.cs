using System.Collections.Generic;

namespace GreenStoreKata
{
    /// <summary>All special items of Alisson's store.</summary>
    public static class ItemListCollections
    {
        ///// <summary>Items in this list will have their quality increase over time.</summary>
        public static List<string> AppreciatingItems = new() { ItemList.AgedCheddar };
        public static List<string> VipTicketItemsWithExpiration = new() { ItemList.VIPTicketsToASamuraiConcert };
        public static List<string> LegendaryItems = new() { ItemList.Longclaw };
    }
}