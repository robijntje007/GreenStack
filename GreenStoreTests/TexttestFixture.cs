
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

            var items = new List<Item>{
                new Item {Name = ItemList.ThreeAgilityVest, SellIn = 10, Quality = 20},
                new Item {Name = ItemList.AgedCheddar, SellIn = 2, Quality = 0},
                new Item {Name = ItemList.ElixirOfTheBear, SellIn = 5, Quality = 7},
                new Item {Name = ItemList.Longclaw, SellIn = 0, Quality = 80},
                new Item {Name = ItemList.Longclaw, SellIn = -1, Quality = 80},
                new Item
                {
                    Name = ItemList.VIPTicketsToASamuraiConcert,
                    SellIn = 15,
                    Quality = 20
                },
                new Item
                {
                    Name = ItemList.VIPTicketsToASamuraiConcert,
                    SellIn = 10,
                    Quality = 49
                },
                new Item
                {
                    Name = ItemList.VIPTicketsToASamuraiConcert,
                    SellIn = 5,
                    Quality = 49
                },
                // this conjured item does not work properly yet
                new Item {Name = ItemList.ConjuredCharismaPotion, SellIn = 3, Quality = 6}
            };

            var app = new GreenStoreKata.GreenStore(items);

            int days = 30;
            if (args.Length > 0)
            {
                days = int.Parse(args[0]) + 1;
            }

            for (var i = 0; i < days; i++)
            {
                Console.WriteLine("-------- day " + i + " --------");
                //Console.WriteLine("name, sellIn, quality");
                WriteName("name");
                Console.Write(", ");
                WriteSellIn("sellIn");
                Console.Write(", ");
                WriteQuality("quality");
                Console.WriteLine("");

                for (var j = 0; j < items.Count; j++)
                {
                    WriteName(items[j].Name);
                    Console.Write(", ");
                    WriteSellIn(items[j].SellIn.ToString());
                    Console.Write(", ");
                    WriteQuality(items[j].Quality.ToString());
                    Console.WriteLine("");
                    //System.Console.WriteLine(items[j].Name + ", " + items[j].SellIn + ", " + items[j].Quality);
                }
                Console.WriteLine("");
                app.UpdateQuality();
            }

        }

        private static void WriteName(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(text);
            ResetColor();
        }
        private static void WriteSellIn(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            ResetColor();
        }
        private static void WriteQuality(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(text);
            ResetColor();

        }
        private static void ResetColor() =>
            Console.ForegroundColor = DefaultColor;

        private static ConsoleColor DefaultColor = ConsoleColor.White;
    }
}
