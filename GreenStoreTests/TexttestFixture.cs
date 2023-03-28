
using GreenStoreKata;

using System;
using System.Collections.Generic;

namespace GreenStoreTests
{
    public static class TexttestFixture
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("OMGHAI!");

            IList<Item> Items = new List<Item>{
                new Item {Name = "+3 Agility Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Cheddar", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Bear", SellIn = 5, Quality = 7},
                new Item {Name = "Longclaw", SellIn = 0, Quality = 80},
                new Item {Name = "Longclaw", SellIn = -1, Quality = 80},
                new Item
                {
                    Name = "VIP tickets to a Samurai concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = "VIP tickets to a Samurai concert",
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = "VIP tickets to a Samurai concert",
                    SellIn = 5,
                    Quality = 49
                },
                // this conjured item does not work properly yet
                new Item {Name = "Conjured Charisma Potion", SellIn = 3, Quality = 6}
            };

            var app = new GreenStore(Items);

            int days = 30;
            if (args.Length > 0)
            {
                days = int.Parse(args[0]) + 1;
            }

            for (var i = 0; i < days; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < Items.Count; j++)
                {
                    System.Console.WriteLine(Items[j].Name + ", " + Items[j].SellIn + ", " + Items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }
        }
    }
}
