using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ITDranik.CodingInterview.Solvers.Caching
{
    internal class LfuItem<TKey, TValue>
    {
        public LfuItem(TKey key, TValue value, int frequency)
        {
            Key = key;
            Value = value;
            Frequency = frequency;
        }

        public TKey Key { get; }
        public TValue Value { get; }
        public int Frequency { get; }
    }

    internal class LfuFrequencyGroup<TKey, TValue>
    {
        public LfuFrequencyGroup(int frequency)
        {
            Frequency = frequency;
            Items = new LinkedList<LfuItem<TKey, TValue>>();
        }

        public int Frequency { get; }
        public LinkedList<LfuItem<TKey, TValue>> Items { get; }
    }

    public class LfuCache<TKey, TValue>
    {
        public LfuCache(int capacity)
        {
            if (capacity <= 0)
            {
                var exMessage = $"must be a positive value";
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, exMessage);
            }

            _capacity = capacity;
            _itemNodesByKey = new Dictionary<TKey, LinkedListNode<LfuItem<TKey, TValue>>>();
            _frequencyGroupsByFrequency = new Dictionary<
                int,
                LinkedListNode<LfuFrequencyGroup<TKey, TValue>>
            >();
            _frequencyGroups = new LinkedList<LfuFrequencyGroup<TKey, TValue>>();
        }

        public void Add(TKey key, TValue value)
        {
            if (_itemNodesByKey.ContainsKey(key))
            {
                Update(key, value);
            }
            else if (_itemNodesByKey.Count < _capacity)
            {
                AddNew(key, value);
            }
            else
            {
                Evict();
                AddNew(key, value);
            }
        }

        private void AddNew(TKey key, TValue value)
        {
            var lfuItem = new LfuItem<TKey, TValue>(key, value, 1);

            if (!_frequencyGroupsByFrequency.TryGetValue(1, out var frequencyGroupNode))
            {
                frequencyGroupNode = _frequencyGroups
                    .AddFirst(new LfuFrequencyGroup<TKey, TValue>(1));
                _frequencyGroupsByFrequency.Add(1, frequencyGroupNode);
            }

            _itemNodesByKey[key] = frequencyGroupNode.Value.Items.AddLast(lfuItem);
        }

        private void Update(TKey key, TValue value)
        {
            var itemNode = _itemNodesByKey[key];
            var frequency = itemNode.Value.Frequency;
            var frequencyNode = _frequencyGroupsByFrequency[itemNode.Value.Frequency];
            var nextFrequency = frequency + 1;

            if (!_frequencyGroupsByFrequency.TryGetValue(nextFrequency, out var nextFrequencyNode))
            {
                nextFrequencyNode = _frequencyGroups.AddAfter(
                    frequencyNode,
                    new LfuFrequencyGroup<TKey, TValue>(nextFrequency)
                );
                _frequencyGroupsByFrequency[nextFrequency] = nextFrequencyNode;
            }
            Remove(key);

            var nextItemNode = nextFrequencyNode.Value.Items.AddLast(new LfuItem<TKey, TValue>(
                key,
                value,
                nextFrequency
            ));
            _itemNodesByKey[key] = nextItemNode;
        }

        private void Evict()
        {
            var lfuItemNode = _frequencyGroups.First.Value.Items.First;
            Remove(lfuItemNode.Value.Key);
        }

        public bool TryGet(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (_itemNodesByKey.TryGetValue(key, out var itemNode))
            {
                value = itemNode.Value.Value;
                Update(key, value);
                return true;
            }

            value = default!;
            return false;
        }

        public bool Remove(TKey key)
        {
            if (_itemNodesByKey.TryGetValue(key, out var itemNode))
            {
                var frequencyGroupNode = _frequencyGroupsByFrequency[itemNode.Value.Frequency];
                frequencyGroupNode.Value.Items.Remove(itemNode);
                if (frequencyGroupNode.Value.Items.Count == 0)
                {
                    _frequencyGroupsByFrequency.Remove(itemNode.Value.Frequency);
                    _frequencyGroups.Remove(frequencyGroupNode);
                }
                _itemNodesByKey.Remove(key);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            _itemNodesByKey.Clear();
            _frequencyGroups.Clear();
            _frequencyGroupsByFrequency.Clear();
        }

        private readonly Dictionary<TKey, LinkedListNode<LfuItem<TKey, TValue>>> _itemNodesByKey;
        private readonly Dictionary<
            int,
            LinkedListNode<LfuFrequencyGroup<TKey, TValue>>
        > _frequencyGroupsByFrequency;
        private readonly LinkedList<LfuFrequencyGroup<TKey, TValue>> _frequencyGroups;
        private readonly int _capacity;
    }

}
