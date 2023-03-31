using Xunit;
using GreenStoreKata;
using FluentAssertions;
using Bogus;
using AutoBogus;
using System;
using System.Linq;

namespace GreenStoreTests
{
    public class GreenStoreTest
    {
        readonly Faker _faker;
        private const int QualityCeiling = 50;
        private const int LegendaryQualityCeiling = 80;
        private const int QualityFloor = 0;
        private readonly Func<Item, bool> selItemlAsap = x => x.SellIn == 0;
        private readonly Func<Item, bool> itemHasNoQuality = x => x.Quality == QualityFloor;
        private const int FirstVipThreshold = 10;
        private const int SecondVipThreshold = 5;
        private const int NumberOfTestCases = 10;
        public GreenStoreTest() =>
            _faker = new Faker();

        [Fact]
        public void NormalItemsShouldHaveAValueOfZeroWhenLongInstock()
        {
            var initialQuality = 5;

            var items = new AutoFaker<Item>()
                .RuleFor(x => x.SellIn, initialQuality)
                .RuleFor(x => x.Quality, initialQuality)
                .Generate(NumberOfTestCases);

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < initialQuality; i++)
                app.UpdateQuality();

            items.ShouldAll().HaveQualityOf(0);
        }


        [Fact]
        public void LegendaryItemsShoulRemainSellInDateAndQuality()
        {
            var randomValue = _faker.Random.Number(QualityFloor, LegendaryQualityCeiling);

            var items = new AutoFaker<Item>()
                .RuleFor(x => x.Name, x => x.PickRandom(ItemListCollections.LegendaryItems))
                .RuleFor(x => x.SellIn, randomValue)
                .RuleFor(x => x.Quality, randomValue)
                .Generate(NumberOfTestCases);

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < randomValue; i++)
                app.UpdateQuality();

            items.ShouldAll().HaveQualityOf(randomValue).And.HaveSellInOf(randomValue);
        }

        [Fact]
        public void VipTicketShoulIncreaseInQualitySteps()
        {
            var initialQuality = FirstVipThreshold + 2;

            var items = new AutoFaker<Item>()
                .RuleFor(x => x.Name, x => x.PickRandom(ItemListCollections.VipTicketItemsWithExpiration))
                .RuleFor(x => x.SellIn, initialQuality)
                .RuleFor(x => x.Quality, initialQuality)
                .Generate(NumberOfTestCases);

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < initialQuality; i++)
            {
                app.UpdateQuality();
                switch (i)
                {
                    case 0:
                        items.ShouldAll().HaveQualityOf(13).And.HaveSellInOf(11);
                        break;
                    case 1:
                        items.ShouldAll().HaveQualityOf(15).And.HaveSellInOf(FirstVipThreshold);
                        break;
                    case 5:
                        items.ShouldAll().HaveQualityOf(23).And.HaveSellInOf(6);
                        break;
                    case 6:
                        items.ShouldAll().HaveQualityOf(26).And.HaveSellInOf(SecondVipThreshold);

                        break;
                    case 7:
                        items.ShouldAll().HaveQualityOf(29).And.HaveSellInOf(4);

                        break;
                    case 10:
                        items.ShouldAll().HaveQualityOf(38).And.HaveSellInOf(1);//Last  day of selling
                        break;
                    case >= 11:
                        items.ShouldAll().HaveQualityOf(0).And.HaveSellInOf(1);//Past due
                        break;
                }
            }
        }

        [Fact]
        public void RegularItemsShouldDegredateFasterAfterDueDate()
        {
            var sellItemTodayFaker = new AutoFaker<Item>()
                .RuleFor(x => x.Name, x => x.Random.Word())
                .RuleFor(x => x.SellIn, 0);

            var items = sellItemTodayFaker
                .RuleFor(x => x.Quality, x => x.Random.Int(QualityFloor, QualityCeiling))
                .Generate(NumberOfTestCases);

            items.Add(sellItemTodayFaker.RuleFor(x => x.Quality, 4));

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < 5; i++)
            {
                app.UpdateQuality();
                items.Should().OnlyContain(x => selItemlAsap(x));
                var cornerCase = items.Last();
                switch (i)
                {
                    case 0:
                        cornerCase.Quality.Should().Be(2);
                        break;
                    default:
                        itemHasNoQuality(cornerCase).Should().BeTrue();
                        break;
                }
            }
        }

        [Fact]
        public void ConjuredItemsShouldDegredateFaster()
        {
            var items = new AutoFaker<Item>()
                .RuleFor(x => x.Name, x => "Conjured" + x.Random.Word())
                .RuleFor(x => x.Quality, 6)
                .RuleFor(x => x.SellIn, 4)
                .Generate(NumberOfTestCases);

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < 5; i++)
            {
                app.UpdateQuality();

                switch (i)
                {
                    case 0:
                        items.ShouldAll().HaveQualityOf(4);
                        break;
                    case 1:
                        items.ShouldAll().HaveQualityOf(2);
                        break;
                    default:
                        items.ShouldAll().HaveQualityOf(0);
                        break;
                }
            }
        }

        [Fact]
        public void AppreciatingItemsShouldAppreciateAndDecrease()
        {
            var items = new AutoFaker<Item>()
                .RuleFor(x => x.Name, x => x.PickRandom(ItemListCollections.AppreciatingItems))
                .RuleFor(x => x.Quality, 7)
                .RuleFor(x => x.SellIn, 2)
                .Generate(NumberOfTestCases);

            var app = new GreenStoreKata.GreenStore(items);

            for (int i = 0; i < 5; i++)
            {
                app.UpdateQuality();

                switch (i)
                {
                    case 0:
                        items.ShouldAll().HaveQualityOf(8).And.HaveSellInOf(1);
                        break;
                    case 1:
                        items.ShouldAll().HaveQualityOf(6).And.HaveSellInOf(0);
                        break;
                    case 2:
                        items.ShouldAll().HaveQualityOf(4).And.HaveSellInOf(0);
                        break;
                    case >= 4:
                        items.ShouldAll().HaveQualityOf(0).And.HaveSellInOf(0);
                        break;
                }
            }
        }
    }
}