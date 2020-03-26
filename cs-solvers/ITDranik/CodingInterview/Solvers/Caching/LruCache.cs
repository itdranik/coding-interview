using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ITDranik.CodingInterview.Solvers.Caching {
    internal class LruItem<TKey, TValue> {
        public LruItem(TKey key, TValue value) {
            Key = key;
            Value = value;
        }

        public TKey Key { get; }
        public TValue Value { get; }
    }

    public class LruCache<TKey, TValue> {
        public LruCache(int capacity) {
            if (capacity <= 0) {
                var exMessage = $"must be a positive value";
                throw new System.ArgumentOutOfRangeException(nameof(capacity), capacity, exMessage);
            }

            _capacity = capacity;
            _itemNodesByKey = new Dictionary<TKey, LinkedListNode<LruItem<TKey, TValue>>>();
            _items = new LinkedList<LruItem<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value) {
            if (_itemNodesByKey.ContainsKey(key)) {
                Update(key, value);
            } else if (_itemNodesByKey.Count < _capacity) {
                AddNew(key, value);
            } else {
                Evict();
                AddNew(key, value);
            }
        }

        private void AddNew(TKey key, TValue value) {
            var entry = new LruItem<TKey, TValue>(key, value);
            _itemNodesByKey[key] = _items.AddFirst(entry);
        }

        private void Update(TKey key, TValue value) {
            Remove(key);
            AddNew(key, value);
        }

        private void Evict() {
            var minKey = _items.Last.Value.Key;
            Remove(minKey);
        }

        public bool TryGet(TKey key, [MaybeNullWhen(false)] out TValue value) {
            if (_itemNodesByKey.TryGetValue(key, out var node)) {
                value = node.Value.Value;
                Update(key, value);
                return true;
            }

            value = default!;
            return false;
        }

        public bool Remove(TKey key) {
            if (_itemNodesByKey.TryGetValue(key, out var node)) {
                _items.Remove(node);
                return _itemNodesByKey.Remove(key);
            }

            return false;
        }

        public void Clear() {
            _items.Clear();
            _itemNodesByKey.Clear();
        }

        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<LruItem<TKey, TValue>>> _itemNodesByKey;
        private readonly LinkedList<LruItem<TKey, TValue>> _items;
    }
}
