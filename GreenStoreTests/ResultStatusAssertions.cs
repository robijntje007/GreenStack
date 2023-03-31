using System.Collections.Generic;
using GreenStoreKata;
using FluentAssertions;
using System.Linq;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace GreenStoreTests
{
    public class ResultStatusAssertions : ReferenceTypeAssertions<IEnumerable<Item>, ResultStatusAssertions>
    {
        public ResultStatusAssertions(IEnumerable<Item> instance) : base(instance) { }

        protected override string Identifier => "ResultStatus";


        public AndConstraint<ResultStatusAssertions> HaveQualityOf(int quality)
        {
            Execute.Assertion
              .Given(() => Subject)
              .ForCondition(status => status.All(x => x.Quality == quality));

            return new AndConstraint<ResultStatusAssertions>(this);
        }

        public AndConstraint<ResultStatusAssertions> HaveSellInOf(int sellIn)
        {
            Execute.Assertion
              .Given(() => Subject)
              .ForCondition(status => status.All(x => x.SellIn == sellIn));

            return new AndConstraint<ResultStatusAssertions>(this);
        }
    }
}