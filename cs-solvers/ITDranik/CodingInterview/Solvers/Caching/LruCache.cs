using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ITDranik.CodingInterview.Solvers.Caching {
    internal class LruEntry<TKey, TValue> {

        public LruEntry(TKey key, TValue value) {
            _key = key;
            _value = value;
        }

        public TKey Key { get { return _key; } }
        public TValue Value { get { return _value; } }

        private readonly TKey _key;
        private readonly TValue _value;
    }

    public class LruCache<TKey, TValue> {
        public LruCache(int capacity) {
            if (capacity <= 0) {
                var exMessage = $"must be a positive value";
                throw new System.ArgumentOutOfRangeException(nameof(capacity), capacity, exMessage);
            }

            _capacity = capacity;
            _cache = new Dictionary<TKey, LinkedListNode<LruEntry<TKey, TValue>>>();
            _nodes = new LinkedList<LruEntry<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value) {
            if (_cache.Count < _capacity) {
                AddNewEntry(key, value);
            } else if (_cache.ContainsKey(key)) {
                UpdateEntry(key, value);
            } else {
                ReplaceOldEntry(key, value);
            }
        }

        private void AddNewEntry(TKey key, TValue value) {
            var entry = new LruEntry<TKey, TValue>(key, value);
            _cache[key] = _nodes.AddFirst(entry);
        }

        private void UpdateEntry(TKey key, TValue value) {
            Remove(key);
            AddNewEntry(key, value);
        }

        private void ReplaceOldEntry(TKey key, TValue value) {
            var minKey = _nodes.Last.Value.Key;
            Remove(minKey);
            AddNewEntry(key, value);
        }

        public bool TryGet(TKey key, [MaybeNullWhen(false)] out TValue value) {
            if (!_cache.ContainsKey(key)) {
                value = default!;
                return false;
            }

            value = GetEntry(key);
            return true;
        }

        private TValue GetEntry(TKey key) {
            var node = _cache[key];
            Remove(key);
            var entry = node.Value;
            AddNewEntry(entry.Key, entry.Value);
            return entry.Value;
        }

        public bool Remove(TKey key) {
            if (_cache.TryGetValue(key, out var node)) {
                _nodes.Remove(node);
                return _cache.Remove(key);
            }

            return false;
        }

        public void Clear() {
            _nodes.Clear();
            _cache.Clear();
        }

        private readonly int _capacity;
        private readonly Dictionary<TKey, LinkedListNode<LruEntry<TKey, TValue>>> _cache;
        private readonly LinkedList<LruEntry<TKey, TValue>> _nodes;
    }
}
