namespace GreenStoreKata
{
    public class SellInCalculator
    {
        private const int SellInFloor = 0;

        private static Item AdhereToSellInFloor(Item item)
        {
            if (item.SellIn < SellInFloor)
                item.SellIn = SellInFloor;
            return item;
        }

        public static Item SetSellIn(Item item)
        {
            //current assumption is that "never has to be sold" means:
            //same value that adheres to the floor
            if (!item.IsLegendaryItem())
                item.SellIn--;

            return AdhereToSellInFloor(item);
        }
    }
}