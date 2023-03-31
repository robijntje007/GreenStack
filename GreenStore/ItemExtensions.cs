namespace GreenStoreKata
{
    public static class ItemExtensions
    {
        /// <remarks>Current assumption is that "conjured items" are items which contain "conjured" in the name</remarks>
        public static bool IsConjuredItem(this Item item) =>
            item.Name.Contains("conjured", System.StringComparison.InvariantCultureIgnoreCase);

        /// <remarks>Like cheddar</remarks>
        public static bool IsAppreciatingItem(this Item item) =>
            ItemListCollections.AppreciatingItems.Contains(item.Name);

        public static bool IsLegendaryItem(this Item item) =>
            ItemListCollections.LegendaryItems.Contains(item.Name);

        public static bool IsVipTicketItemWithExpiration(this Item item) =>
            ItemListCollections.VipTicketItemsWithExpiration.Contains(item.Name);

        /// <summary>Determine wheter the item is "normal" meaning: <br/>
        /// <list type="bullet">
        /// <item>Not a Legendary item </item>
        /// <item>Not a Conjured item</item>
        /// <item>Not a appreciating</item>
        /// <item>Not a Vip ticket item</item>
        /// </list>
        /// </summary>
        public static bool IsNormalItem(this Item item) =>
            (
            !IsLegendaryItem(item) &&
            !IsConjuredItem(item) &&
            !IsAppreciatingItem(item) &&
            !IsVipTicketItemWithExpiration(item));

    }
}