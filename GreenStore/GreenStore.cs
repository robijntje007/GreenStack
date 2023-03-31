using System.Collections.Generic;
namespace GreenStoreKata
{
    public class GreenStore
    {
        readonly IList<Item> _items;
        public GreenStore(IList<Item> Items) =>
            _items = Items;

        /// <summary>End of day processing <br/>
        /// This will update the values of the given items.
        /// </summary>
        public void UpdateQuality()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                _items[i] = SellInCalculator.SetSellIn(_items[i]);
                _items[i] = QualityCalculator.SetQuality(_items[i]);
            }
        }
    }
}