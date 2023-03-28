using Xunit;
using System.Collections.Generic;
using GreenStoreKata;

namespace GreenStoreTests
{
    public class GreenStoreTest
    {
        [Fact]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GreenStore app = new GreenStore(Items);
            app.UpdateQuality();
            Assert.Equal("fixme", Items[0].Name);
        }
    }
}
