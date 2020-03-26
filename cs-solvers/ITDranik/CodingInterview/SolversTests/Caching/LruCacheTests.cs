using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Caching;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Caching {
    public class LruCacheTests {
        [Fact]
        public void GetNonexistentItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.TryGet("item", out var _).Should().BeFalse();
        }

        [Fact]
        public void AddItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item", 1);

            cache.TryGet("item", out var actual).Should().BeTrue();
            actual.Should().Be(1);
        }

        [Fact]
        public void OverwriteItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item", 1);
            cache.Add("item", 2);

            cache.TryGet("item", out var actual).Should().BeTrue();
            actual.Should().Be(2);
        }

        [Fact]
        public void EvictionAfterOverwriteTest() {
            var cache = new LruCache<string, int>(1);
            cache.Add("item1", 1);
            cache.Add("item1", 1);
            cache.Add("item2", 2);
            cache.Add("item3", 3);

            cache.TryGet("item1", out var item1).Should().BeFalse();
            cache.TryGet("item2", out var item2).Should().BeFalse();
            cache.TryGet("item3", out var item3).Should().BeTrue();
            item3.Should().Be(3);
        }

        [Fact]
        public void RemoveItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item", 1);
            cache.Remove("item").Should().BeTrue();

            cache.TryGet("item", out var _).Should().BeFalse();
        }

        [Fact]
        public void RemoveNonexistentItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.Remove("item").Should().BeFalse();
        }

        [Fact]
        public void ClearCacheTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item1", 1);
            cache.Add("item2", 2);

            cache.Clear();

            cache.TryGet("item1", out var _1).Should().BeFalse();
            cache.TryGet("item2", out var _2).Should().BeFalse();
        }

        [Fact]
        public void EvictItemTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item1", 1);
            cache.Add("item2", 2);
            cache.Add("item3", 3);
            cache.Add("item4", 4);

            cache.TryGet("item1", out var item1).Should().BeFalse();
            cache.TryGet("item2", out var item2).Should().BeTrue();
            item2.Should().Be(2);
            cache.TryGet("item3", out var item3).Should().BeTrue();
            item3.Should().Be(3);
            cache.TryGet("item4", out var item4).Should().BeTrue();
            item4.Should().Be(4);
        }

        [Fact]
        public void EvictItemWhenPriorityIsChangedByGetTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item1", 1);
            cache.Add("item2", 2);
            cache.Add("item3", 3);
            cache.TryGet("item1", out var _);
            cache.Add("item4", 4);

            cache.TryGet("item1", out var item1).Should().BeTrue();
            item1.Should().Be(1);
            cache.TryGet("item2", out var item2).Should().BeFalse();
            cache.TryGet("item3", out var item3).Should().BeTrue();
            item3.Should().Be(3);
            cache.TryGet("item4", out var item4).Should().BeTrue();
            item4.Should().Be(4);
        }

        [Fact]
        public void EvictItemWhenPriorityIsChangedByUpdateTest() {
            var cache = new LruCache<string, int>(3);
            cache.Add("item1", 1);
            cache.Add("item2", 2);
            cache.Add("item3", 3);
            cache.Add("item1", 1);
            cache.Add("item4", 4);

            cache.TryGet("item1", out var item1).Should().BeTrue();
            item1.Should().Be(1);
            cache.TryGet("item2", out var item2).Should().BeFalse();
            cache.TryGet("item3", out var item3).Should().BeTrue();
            item3.Should().Be(3);
            cache.TryGet("item4", out var item4).Should().BeTrue();
            item4.Should().Be(4);
        }
    }
}
