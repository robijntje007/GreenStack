using System.Collections.Generic;
using GreenStoreKata;
namespace GreenStoreTests
{
    /// <summary>Entrypoint for the custum FluentAssertions.</summary>
    public static class ResultStatusExtensions
    {
        public static ResultStatusAssertions ShouldAll(this IEnumerable<Item> instance) =>
            new(instance);
    }
}