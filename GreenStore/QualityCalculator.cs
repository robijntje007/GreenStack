namespace GreenStoreKata
{
    public class QualityCalculator
    {
        private const int QualityCeiling = 50;
        private const int QualityFloor = 0;
        private const int LegendaryFixedQuality = 80;
        private const int NormalDegradation = 1;
        private const int ExpiredDegradation = NormalDegradation * 2;
        private const int ConjuredDegradation = NormalDegradation * 2;

        private const int FirstVipThreshold = 10;
        private const int FirstVipAppreciation = 2;

        private const int SecondVipThreshold = 5;
        private const int SecondVipAppreciation = 3;

        private const int DefaultVipTicketAppreciation = 1;

        /// <summary>An item's quality must adhere to the following:
        /// <list type="bullet">
        /// <item>It cannot be less than the <see cref="QualityFloor"/></item>
        /// <item>It cannot be more than the <see cref="QualityCeiling"/></item>
        /// </list>
        /// </summary>
        private static Item AdhereToFloorAndCeiling(Item item)
        {
            if (item.IsLegendaryItem())
            {
                //current assumption is that legendary items don't change
                //so the given value will stay
                //if this isn't the case the following should be enabled
                //item.Quality = LegendaryFixedQuality;

                return item;
            }

            if (item.Quality >= QualityCeiling)//Adhere to ceiling
                item.Quality = QualityCeiling;

            if (item.Quality <= QualityFloor)//Adhere to floor
                item.Quality = QualityFloor;

            return item;
        }

        /// <summary>Vip items increase in value. With threshold towards SellIn date</summary>
        private static Item SetVipTicketItemQuality(Item item)
        {
            if (item.SellIn == 0)
            {
                item.Quality = QualityFloor;
                return item;
            }

            int step;
            switch (item.SellIn)
            {
                case <= SecondVipThreshold:
                    step = SecondVipAppreciation;
                    break;
                case <= FirstVipThreshold:
                    step = FirstVipAppreciation;
                    break;
                default:
                    step = DefaultVipTicketAppreciation;
                    break;
            }
            item.Quality += step;
            return item;
        }

        /// <summary>Normal items decrease their quality by 1.</summary>
        /// <remarks>Assumption "Quality degrades twice as fast" means 2x the regular step.<br/>
        /// Not increasingly more degradation.
        /// </remarks>
        private static Item SetNormalQuality(Item item)
        {
            var step = NormalDegradation;
            if (item.SellIn <= 0)
                step = ExpiredDegradation;

            item.Quality -= step;

            return item;
        }

        private static Item SetConjuredQuality(Item item)
        {
            item.Quality -= ConjuredDegradation;
            return item;
        }

        /// <summary>Legendary items don't decrease in quality</summary>
        private static Item SetLegendaryQuality(Item item) =>
            item;

        private static Item SetAppreciatingQuality(Item item)
        {
            if (item.SellIn == 0)
                item.Quality -= ExpiredDegradation;
            else
                item.Quality++;

            return item;
        }

        public static Item SetQuality(Item item)
        {
            if (item.IsLegendaryItem())
                item = SetLegendaryQuality(item);
            else if (item.IsConjuredItem())
                item = SetConjuredQuality(item);
            else if (item.IsNormalItem())
                item = SetNormalQuality(item);
            else if (item.IsVipTicketItemWithExpiration())
                item = SetVipTicketItemQuality(item);
            else if (item.IsAppreciatingItem())
                item = SetAppreciatingQuality(item);

            return AdhereToFloorAndCeiling(item);
        }
    }
}